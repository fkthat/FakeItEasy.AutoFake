using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FakeItEasy.AutoFake
{
    /// <summary>
    /// Resolves value by matching <c cref="IParameter"/> array.
    /// </summary>
    internal class MatchParameterResolver : ChainParameterResolver
    {
        private readonly IEnumerable<IParameter> _parameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="MatchParameterResolver"/> class.
        /// </summary>
        /// <param name="next">The next value resolver in the chain.</param>
        /// <param name="parameters">The parameters.</param>
        public MatchParameterResolver(IParameterResolver next, IEnumerable<IParameter> parameters) : base(next)
        {
            _parameters = parameters;
        }

        /// <summary>
        /// Tries the resolve the value of the specified parameter.
        /// </summary>
        /// <param name="parameterInfo">The parameter information.</param>
        /// <returns>The resolved value if it can to resolve it. Otherwise returns null.</returns>
        protected override ParameterValue? TryGetValue(ParameterInfo parameterInfo) =>
            _parameters.Where(p => p.Match(parameterInfo))
                .Select(p => new SuccessParameterValue(p.GetValue(parameterInfo)))
                .FirstOrDefault();
    }
}
