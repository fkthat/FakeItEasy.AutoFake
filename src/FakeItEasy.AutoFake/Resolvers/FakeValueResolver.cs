using System;
using System.Reflection;
using FakeItEasy.AutoFake.Fakes;
using FakeItEasy.AutoFake.Parameters;

namespace FakeItEasy.AutoFake.Resolvers
{
    internal class FakeValueResolver : ChainValueResolver
    {
        private readonly IFakeFactory _fakeFactory;

        public FakeValueResolver(IFakeFactory fakeFactory, IValueResolver next) : base(next)
        {
            _fakeFactory = fakeFactory;
        }

        protected override ResolvedValue? TryResolve(ParameterInfo parameterInfo,
            params IParameter[] parameters)
        {
            var v = _fakeFactory.Get(parameterInfo.ParameterType);
            return v != null ? new ResolvedSuccessValue(v) : null;
        }
    }
}
