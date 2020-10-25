using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeItEasy.AutoFake
{
    public static class AutoFakerExtensions
    {
        /// <summary>
        /// Creates the instance of the <typeparamref name="T"/> type.
        /// </summary>
        /// <returns>The created instance.</returns>
        public static T CreateInstance<T>(this IAutoFaker autoFaker, params IParameter[] parameters)
            => (T)autoFaker.CreateInstance(typeof(T), parameters);

        /// <summary>
        /// Gets the service that will be provided by the AutoFake container. If the service of the
        /// <typeparamref name="T"/> type is provided by <see cref="IAutoMockerConfiguration.Use"/>
        /// it will return the instance provided. If a fake service of the <typeparamref name="T"/>
        /// wasn't created yet it will create and return a new fake.
        /// </summary>
        /// <returns>The service instance.</returns>
        public static T Get<T>(this IAutoFaker autoFaker) where T : class =>
            (T)autoFaker.Get(typeof(T));
    }
}
