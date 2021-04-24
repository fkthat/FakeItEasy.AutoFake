using System;
using System.Reflection;

namespace FakeItEasy.AutoFake.Parameters
{
    /// <summary>
    /// Matches and resolves a parameter by predicates.
    /// </summary>
    public class ResolvedParameter : IParameter
    {
        private readonly Func<ParameterInfo, bool> _match;
        private readonly Func<ParameterInfo, object?> _resolve;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolvedParameter"/> class.
        /// </summary>
        /// <param name="match">The match predicate.</param>
        /// <param name="resolve">The resolve function.</param>
        public ResolvedParameter(
            Func<ParameterInfo, bool> match,
            Func<ParameterInfo, object?> resolve)
        {
            _match = match;
            _resolve = resolve;
        }

        /// <summary>
        /// Tries to resolve parameter value.
        /// </summary>
        /// <param name="parameterInfo">The parameter information.</param>
        /// <param name="value">The resolved value on success, <c cref="false"/> otherwise.</param>
        /// <returns>
        /// <c cref="true"/> if the parameter successfully resolved, <c cref="false"/> otherwise.
        /// </returns>
        public bool TryResolve(ParameterInfo parameterInfo, out object? value)
        {
            var match = _match(parameterInfo);
            value = match ? _resolve(parameterInfo) : null;
            return match;
        }
    }
}
