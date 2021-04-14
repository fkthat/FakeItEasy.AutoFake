using System;
using FakeItEasy.AutoFake.Fakes;
using FakeItEasy.AutoFake.Parameters;

namespace FakeItEasy.AutoFake
{
    /// <summary>
    /// An auto-mocking IoC container that generates fake objects using FakeItEasy.
    /// </summary>
    public class AutoFaker : IAutoFaker
    {
        private readonly IFakeFactory _fakeFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoFaker"/> class.
        /// </summary>
        public AutoFaker(Action<IAutoFakerConfiguration>? configurationBuilder = null)
        {
            AutoFakerConfiguration configuration = new();
            configurationBuilder?.Invoke(configuration);

            _fakeFactory = new FakeContainer(new FakeFactory());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoFaker"/> class. This constructor is
        /// intended for testing.
        /// </summary>
        internal AutoFaker(IFakeFactory fakeFactory)
        {
            _fakeFactory = fakeFactory;
        }

        /// <summary>
        /// Creates the instance of the <paramref name="type"/> type.
        /// </summary>
        /// <param name="type">The type of an instance to create.</param>
        /// <param name="parameters">
        /// The parameters to pass to the <paramref name="type"/> constructor.
        /// </param>
        /// <returns>The created instance.</returns>
        public object CreateInstance(Type type, params IParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the service that will be provided by the AutoFake container. If a fake service of
        /// the <paramref name="type"/> wasn't created yet it will create and return a new fake.
        /// </summary>
        /// <param name="type">The type of a service.</param>
        /// <returns>The service instance.</returns>
        public object Get(Type type) => _fakeContainer.Get(type);
    }
}
