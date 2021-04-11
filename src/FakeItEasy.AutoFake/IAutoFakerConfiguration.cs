using System;

namespace FakeItEasy.AutoFake
{
    /// <summary>
    /// AutoFaker configuration API.
    /// </summary>
    public interface IAutoFakerConfiguration
    {
        /// <summary>
        /// Provide a predefined instance of a service to inject later to created objects.
        /// </summary>
        /// <param name="type">The service type.</param>
        /// <param name="instance">The service instance.</param>
        IAutoFakerConfiguration Use(Type type, object? instance);
    }
}
