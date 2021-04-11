using System.Reflection;
using FakeItEasy.AutoFake.Parameters;

namespace FakeItEasy.AutoFake.Resolvers
{
    internal abstract class ChainValueResolver : IValueResolver
    {
        private readonly IValueResolver _next;

        public ChainValueResolver(IValueResolver next)
        {
            _next = next;
        }

        public ResolvedValue Resolve(ParameterInfo parameterInfo, params IParameter[] parameters) =>
            TryResolve(parameterInfo, parameters) ?? _next.Resolve(parameterInfo, parameters);

        protected abstract ResolvedValue? TryResolve(ParameterInfo parameterInfo,
            params IParameter[] parameters);
    }
}
