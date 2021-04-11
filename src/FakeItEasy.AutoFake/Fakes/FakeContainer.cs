using System;
using System.Collections.Generic;

namespace FakeItEasy.AutoFake.Fakes
{
    internal class FakeContainer : IFakeFactory
    {
        private readonly IFakeFactory _fakeFactory;
        private readonly Dictionary<Type, object> _container = new();

        public FakeContainer(IFakeFactory fakeFactory)
        {
            _fakeFactory = fakeFactory;
        }

        public object Get(Type type)
        {
            if (!_container.TryGetValue(type, out var value))
            {
                value = _fakeFactory.Get(type);
                _container.Add(type, value);
            }

            return value;
        }
    }
}
