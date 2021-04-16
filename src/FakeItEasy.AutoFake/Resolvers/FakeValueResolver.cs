using System.Reflection;
using FakeItEasy.AutoFake.Parameters;
using FakeItEasy.Core;

namespace FakeItEasy.AutoFake.Resolvers
{
    internal class FakeValueResolver : ChainValueResolver
    {
        private readonly IFakeFactory _fakeFactory;

        public FakeValueResolver(IFakeFactory fakeFactory, IValueResolver next) : base(next)
        {
            _fakeFactory = fakeFactory;
        }

        protected override ResolvedValue? TryResolve(
            ParameterInfo parameterInfo, params IParameter[] parameters)
        {
            try
            {
                return new SuccessResolvedValue(_fakeFactory.CreateFake(parameterInfo.ParameterType));
            }
            catch (FakeCreationException)
            {
                return null;
            }
        }
    }
}
