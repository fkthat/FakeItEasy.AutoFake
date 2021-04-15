using System;
using System.Collections.Generic;

namespace FakeItEasy.AutoFake
{
    /// <summary>
    /// Caches fakes created by the underlying factory.
    /// </summary>
    public class FakeFactoryCachingDecorator : IFakeFactory
    {
        private readonly IFakeFactory _fakeFactory;
        private readonly Dictionary<Type, object> _container = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeFactoryCachingDecorator"/> class.
        /// </summary>
        /// <param name="fakeFactory">The underlying fake factory.</param>
        public FakeFactoryCachingDecorator(IFakeFactory fakeFactory)
        {
            _fakeFactory = fakeFactory;
        }

        /// <summary>
        /// Creates a fake of the specified type or gets it from the cache.
        /// </summary>
        /// <param name="type">The type of the fake.</param>
        public object CreateFake(Type type)
        {
            if (!_container.TryGetValue(type, out var value))
            {
                value = _fakeFactory.CreateFake(type);
                _container.Add(type, value);
            }

            return value;
        }
    }
}
