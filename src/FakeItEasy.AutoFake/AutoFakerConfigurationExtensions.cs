using System;
using System.Diagnostics.CodeAnalysis;

namespace FakeItEasy.AutoFake
{
    /// <summary>
    /// Extends <see cref="IAutoFakerConfiguration"/>.
    /// </summary>
    public static class AutoFakerConfigurationExtensions
    {
        /// <summary>
        /// Provide a predefined instance of a service to inject later to created objects.
        /// </summary>
        /// <typeparam name="T">The service type.</typeparam>
        /// <param name="configuration">The <see cref="IAutoFakerConfiguration"/>.</param>
        /// <param name="instance">The service instance.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="configuration"/> or <paramref name="instance"/> is null.
        /// </exception>
        public static IAutoFakerConfiguration Use<T>(this IAutoFakerConfiguration configuration,
            T instance) where T : class
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return configuration.Use(typeof(T), instance);
        }
    }
}
