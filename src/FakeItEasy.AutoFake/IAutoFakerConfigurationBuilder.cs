using System;

namespace FakeItEasy.AutoFake
{
    /// <summary>
    /// AutoFaker configuration API.
    /// </summary>
    public interface IAutoFakerConfigurationBuilder
    {
        /// <summary>
        /// Provide a predefined instance of a service to inject later to created objects.
        /// </summary>
        /// <param name="type">The service type.</param>
        /// <param name="instance">The service instance.</param>
        IAutoFakerConfigurationBuilder Use(Type type, object? instance);

        /// <summary>
        /// Provide a predefined instance of a service to inject later to created objects.
        /// </summary>
        /// <typeparam name="T">The service type.</typeparam>
        /// <param name="instance">The service instance.</param>
        IAutoFakerConfigurationBuilder Use<T>(T instance) where T : class => Use(typeof(T), instance);
    }
}
