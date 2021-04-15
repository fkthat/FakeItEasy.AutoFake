using FakeItEasy.AutoFake.Parameters;

namespace FakeItEasy.AutoFake
{
    /// <summary>
    /// Extension methods for AutoFake classes.
    /// </summary>
    public static class AutoFakerExtensions
    {
        /// <summary>
        /// Creates the instance of the <typeparamref name="T"/> type.
        /// </summary>
        /// <param name="autoFaker"><c cref="IAutoFaker"/> instance.</param>
        /// <param name="parameters">
        /// The parameters to pass to the <typeparamref name="T"/> constructor.
        /// </param>
        /// <returns>The created instance.</returns>
        public static T CreateInstance<T>(
            this IAutoFaker autoFaker, params IParameter[] parameters) =>
            (T)autoFaker.CreateInstance(typeof(T), parameters);

        /// <summary>
        /// Gets the service that will be provided by the AutoFake container. If a fake service of
        /// the <span class="typeparameter">T</span> wasn't created yet it will create and return a
        /// new fake.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="autoFaker"><c cref="IAutoFaker"/> instance.</param>
        /// <returns>The service instance.</returns>
        public static T Get<T>(this IAutoFaker autoFaker) where T : class =>
            (T)autoFaker.Get(typeof(T));
    }
}
