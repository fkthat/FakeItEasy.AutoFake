using System.Reflection;

namespace FakeItEasy.AutoFake.Resolvers
{
    /// <summary>
    /// A value resolver that always fails. Intended to be the last resolver in the chain.
    /// </summary>
    public class FailedValueResolver : IValueResolver
    {
        /// <summary>
        /// Resolves a parameter value by the specified parameter information.
        /// </summary>
        /// <param name="parameterInfo">The parameter information.</param>
        public ResolvedValue Resolve(ParameterInfo parameterInfo) => new FailedResolvedValue();
    }
}
