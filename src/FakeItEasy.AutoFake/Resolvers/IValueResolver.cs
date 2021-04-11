using System.Reflection;
using FakeItEasy.AutoFake.Parameters;

namespace FakeItEasy.AutoFake.Resolvers
{
    internal interface IValueResolver
    {
        ResolvedValue Resolve(ParameterInfo parameterInfo, params IParameter[] parameters);
    }
}
