using System;

namespace FakeItEasy.AutoFake
{
    public interface IAutoFakerConfigurationBuilder
    {
        /// <summary>
        /// Provide a predefined instance of a service to inject later to created objects.
        /// </summary>
        /// <param name="type">The type of a service.</param>
        /// <param name="instance">The instance of a service.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="type"/> or the <paramref name="instance"/> is null.
        /// </exception>
        IAutoFakerConfigurationBuilder Use(Type type, object? instance);

        /// <summary>
        /// Provide a predefined instance of a service to inject later to created objects.
        /// </summary>
        /// <typeparam name="T">The service type.</typeparam>
        /// <param name="configuration">The <see cref="IAutoFakerConfiguration"/>.</param>
        /// <param name="instance">The service instance.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="configuration"/> or <paramref name="instance"/> is null.
        /// </exception>
        IAutoFakerConfigurationBuilder Use<T>(T instance) =>
            Use(typeof(T), instance);
    }
}
