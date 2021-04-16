using System.Reflection;

namespace FakeItEasy.AutoFake.Resolvers
{
    /// <summary>
    /// A parameter value resolver.
    /// </summary>
    public interface IValueResolver
    {
        /// <summary>
        /// Resolves a parameter value by the specified parameter information.
        /// </summary>
        /// <param name="parameterInfo">The parameter information.</param>
        ResolvedValue Resolve(ParameterInfo parameterInfo);
    }
}
