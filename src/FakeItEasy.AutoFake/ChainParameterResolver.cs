using System.Reflection;

namespace FakeItEasy.AutoFake
{
    /// <summary>
    /// A value resolver with fallback. It resolves the value if can, otherwise call the next
    /// resolver in the chain.
    /// </summary>
    internal abstract class ChainParameterResolver : IParameterResolver
    {
        private readonly IParameterResolver _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChainParameterResolver"/> class.
        /// </summary>
        /// <param name="next">The next value resolver in the chain.</param>
        protected ChainParameterResolver(IParameterResolver next)
        {
            _next = next;
        }

        /// <summary>
        /// Resolves a parameter value by the specified parameter information.
        /// </summary>
        /// <param name="parameterInfo">The parameter information.</param>
        public ParameterValue GetValue(ParameterInfo parameterInfo) =>
            TryGetValue(parameterInfo) ?? _next.GetValue(parameterInfo);

        /// <summary>
        /// Tries the resolve the value of the specified parameter.
        /// </summary>
        /// <param name="parameterInfo">The parameter information.</param>
        /// <returns>The resolved value if it can to resolve it. Otherwise returns null.</returns>
        protected abstract ParameterValue? TryGetValue(ParameterInfo parameterInfo);
    }
}
