using System.Linq;
using System.Reflection;
using FakeItEasy.AutoFake.Parameters;

namespace FakeItEasy.AutoFake.Resolvers
{
    internal class ParametersValueResolver : ChainValueResolver
    {
        public ParametersValueResolver(IValueResolver next) : base(next)
        {
        }

        protected override ResolvedValue? TryResolve(ParameterInfo parameterInfo,
            params IParameter[] parameters) => parameters.Where(p => p.Match(parameterInfo))
            .Select(p => p.Resolve(parameterInfo)).Select(v => new ResolvedSuccessValue(v))
            .FirstOrDefault();
    }
}
