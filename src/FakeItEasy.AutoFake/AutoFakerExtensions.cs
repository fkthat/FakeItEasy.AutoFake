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
    }
}
