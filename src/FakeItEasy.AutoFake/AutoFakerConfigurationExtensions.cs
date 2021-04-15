using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeItEasy.AutoFake
{
    /// <summary>
    /// Extension methods for <c cref="IAutoFakerConfiguration"/>.
    /// </summary>
    public static class AutoFakerConfigurationExtensions
    {
        /// <summary>
        /// Provide a predefined instance of a service to inject later to created objects.
        /// </summary>
        /// <typeparam name="T">The service type.</typeparam>
        /// <param name="configuration"><c cref="IAutoFakerConfiguration"/> instance.</param>
        /// <param name="instance">The service instance.</param>
        public static IAutoFakerConfiguration Use<T>(
            this IAutoFakerConfiguration configuration, T instance) where T : class =>
            configuration.Use(typeof(T), instance);
    }
}
