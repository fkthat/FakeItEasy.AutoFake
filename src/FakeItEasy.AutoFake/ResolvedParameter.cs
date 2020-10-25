using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FakeItEasy.AutoFake
{
    /// <summary>
    /// The most generic implementation of <see cref="IParameter"/>.
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
        /// Matches the specified parameter information.
        /// </summary>
        /// <param name="parameterInfo">The parameter information.</param>
        /// <returns>
        /// True if the value of specified parameter can be resolved, false otherwise.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Match(ParameterInfo parameterInfo) => _match(parameterInfo);

        /// <summary>
        /// Resolves value for the specified parameter.
        /// </summary>
        /// <param name="parameterInfo">The parameter information.</param>
        /// <returns>Resolved value. <br/></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object? Resolve(ParameterInfo parameterInfo) => _resolve(parameterInfo);
    }
}
