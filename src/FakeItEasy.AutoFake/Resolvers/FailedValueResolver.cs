using System.Reflection;
using FakeItEasy.AutoFake.Parameters;

namespace FakeItEasy.AutoFake.Resolvers
{
    internal class FailedValueResolver : IValueResolver
    {
        public ResolvedValue Resolve(ParameterInfo parameterInfo, params IParameter[] parameters) =>
            new ResolvedFailedValue();
    }
}
