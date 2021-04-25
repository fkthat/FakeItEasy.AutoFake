using System;
using System.Collections.Generic;

namespace FakeItEasy.AutoFake
{
    internal class FakeFactoryCacheDecorator : IFakeFactory
    {
        private readonly IFakeFactory _fakeFactory;
        private Dictionary<Type, object> _cache = new();

        public FakeFactoryCacheDecorator(IFakeFactory fakeFactory)
        {
            _fakeFactory = fakeFactory;
        }

        public object CreateFake(Type type) =>
            _cache.TryGetValue(type, out var fake)
                ? fake
                : _cache[type] = _fakeFactory.CreateFake(type);
    }
}
