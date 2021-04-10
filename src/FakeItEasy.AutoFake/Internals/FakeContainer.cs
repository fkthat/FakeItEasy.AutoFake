using System;
using System.Collections.Generic;

namespace FakeItEasy.AutoFake.Internals
{
    internal class FakeContainer : IFakeFactory
    {
        private readonly IFakeFactory _fakeFactory;
        private readonly Dictionary<Type, object> _container = new();

        public FakeContainer(IFakeFactory fakeFactory)
        {
            _fakeFactory = fakeFactory;
        }

        public object? Get(Type type)
        {
            if (_container.TryGetValue(type, out var value))
            {
                return value;
            }

            value = _fakeFactory.Get(type);

            if (value != null)
            {
                _container.Add(type, value);
            }

            return value;
        }
    }
}
