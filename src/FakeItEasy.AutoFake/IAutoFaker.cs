using System;

namespace FakeItEasy.AutoFake
{
    /// <summary>
    /// AutoMocker API.
    /// </summary>
    public interface IAutoFaker
    {
        /// <summary>
        /// Creates the instance of the <paramref name="type"/> type.
        /// </summary>
        /// <param name="type">The type of an instance to create.</param>
        /// <returns>The created instance.</returns>
        object CreateInstance(Type type, params Parameters.IParameter[] parameters);

        /// <summary>
        /// Gets the service that will be provided by the AutoFake container. If the service of the
        /// <paramref name="type"/> type is provided by <see cref="IAutoMockerConfiguration.Use"/>
        /// it will return the instance provided. If a fake service of the <paramref name="type"/>
        /// wasn't created yet it will create and return a new fake.
        /// </summary>
        /// <param name="type">The type of a service.</param>
        /// <returns>The service instance.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming",
            "CA1716:Identifiers should not match keywords",
            Justification = "Keep this name for 'compatibility' with Moq.AutoMock.")]
        object Get(Type type);
    }
}
