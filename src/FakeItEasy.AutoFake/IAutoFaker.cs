using System;

namespace FakeItEasy.AutoFake
{
    /// <summary>
    /// An auto-mocking IoC container that generates fake objects using FakeItEasy.
    /// </summary>
    public interface IAutoFaker
    {
        /// <summary>
        /// Creates the instance of the <paramref name="type"/> type.
        /// </summary>
        /// <param name="type">The type of an instance to create.</param>
        /// <param name="parameters">
        /// The parameters to pass to the <paramref name="type"/> constructor.
        /// </param>
        /// <returns>The created instance.</returns>
        object CreateInstance(Type type, params IParameter[] parameters);

        /// <summary>
        /// Gets the service that will be provided by the AutoFake container. If a fake service of
        /// the <paramref name="type"/> wasn't created yet it will create and return a new fake.
        /// </summary>
        /// <param name="type">The type of a service.</param>
        /// <returns>The service instance or null if failed.</returns>
        object Get(Type type);

        /// <summary>
        /// Creates the instance of the <typeparamref name="T"/> type.
        /// </summary>
        /// <param name="parameters">
        /// The parameters to pass to the <typeparamref name="T"/> constructor.
        /// </param>
        /// <returns>The created instance.</returns>
        T CreateInstance<T>(params IParameter[] parameters) =>
            (T)CreateInstance(typeof(T), parameters);

        /// <summary>
        /// Gets the service that will be provided by the AutoFake container. If a fake service of
        /// the <span class="typeparameter">T</span> wasn't created yet it will create and return a
        /// new fake.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>The service instance.</returns>
        T Get<T>() where T : class => (T)Get(typeof(T));
    }
}
