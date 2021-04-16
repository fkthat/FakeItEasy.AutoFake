using System.Reflection;
using FakeItEasy.Core;

namespace FakeItEasy.AutoFake.Resolvers
{
    /// <summary>
    /// Resolves the parameter with the fake.
    /// </summary>
    internal class FakeParameterResolver : ChainParameterResolver
    {
        private readonly IFakeFactory _fakeFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeParameterResolver"/> class.
        /// </summary>
        /// <param name="next">The next value resolver in the chain.</param>
        /// <param name="fakeFactory">The fake factory.</param>
        public FakeParameterResolver(IParameterResolver next, IFakeFactory fakeFactory) : base(next)
        {
            _fakeFactory = fakeFactory;
        }

        /// <summary>
        /// Tries the resolve the value of the specified parameter.
        /// </summary>
        /// <param name="parameterInfo">The parameter information.</param>
        /// <returns>The resolved value if it can to resolve it. Otherwise returns null.</returns>
        protected override ParameterValue? TryGetValue(ParameterInfo parameterInfo)
        {
            try
            {
                return new SuccessParameterValue(
                    _fakeFactory.CreateFake(parameterInfo.ParameterType));
            }
            catch (FakeCreationException)
            {
                return null;
            }
        }
    }
}
