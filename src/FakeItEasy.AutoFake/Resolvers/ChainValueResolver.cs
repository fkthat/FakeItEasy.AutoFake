using System.Reflection;

namespace FakeItEasy.AutoFake.Resolvers
{
    /// <summary>
    /// A value resolver with fallback. It resolves the value if can, otherwise call the next
    /// resolver in the chain.
    /// </summary>
    public abstract class ChainValueResolver : IValueResolver
    {
        private readonly IValueResolver _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChainValueResolver"/> class.
        /// </summary>
        /// <param name="next">The next value resolver in the chain.</param>
        public ChainValueResolver(IValueResolver next)
        {
            _next = next;
        }

        /// <summary>
        /// Resolves a parameter value by the specified parameter information.
        /// </summary>
        /// <param name="parameterInfo">The parameter information.</param>
        public ResolvedValue Resolve(ParameterInfo parameterInfo) =>
            TryResolve(parameterInfo) ?? _next.Resolve(parameterInfo);

        /// <summary>
        /// Tries the resolve the value of the specified parameter.
        /// </summary>
        /// <param name="parameterInfo">The parameter information.</param>
        /// <returns>The resolved value if it can to resolve it. Otherwise returns null.</returns>
        protected abstract ResolvedValue? TryResolve(ParameterInfo parameterInfo);
    }
}
