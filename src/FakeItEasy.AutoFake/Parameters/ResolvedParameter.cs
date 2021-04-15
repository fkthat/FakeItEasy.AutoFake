using System;
using System.Reflection;

namespace FakeItEasy.AutoFake.Parameters
{
    /// <summary>
    /// The most generic implementation of <see cref="IParameter"/>.
    /// </summary>
    public class ResolvedParameter : IParameter
    {
        private readonly Func<ParameterInfo, bool> _match;
        private readonly Func<ParameterInfo, object?> _getValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolvedParameter"/> class.
        /// </summary>
        /// <param name="match">The match predicate.</param>
        /// <param name="getValue">The value resolve function.</param>
        public ResolvedParameter(
            Func<ParameterInfo, bool> match,
            Func<ParameterInfo, object?> getValue)
        {
            _match = match;
            _getValue = getValue;
        }

        /// <summary>
        /// Matches the specified parameter information by the predicate passed previously to the
        /// constructor.
        /// </summary>
        /// <param name="parameterInfo">The parameter information.</param>
        public bool Match(ParameterInfo parameterInfo) => _match(parameterInfo);

        /// <summary>
        /// Gets the value for the specified parameter.
        /// </summary>
        /// <param name="parameterInfo">The parameter information.</param>
        public object? GetValue(ParameterInfo parameterInfo) => _getValue(parameterInfo);
    }
}
