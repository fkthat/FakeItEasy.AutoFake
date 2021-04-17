using System;
using System.Reflection;

namespace FakeItEasy.AutoFake
{
    /// <summary>
    /// <see cref="IParameter"/> implementation that resolves to the constant value.
    /// </summary>
    public class ConstantValueParameter : ResolvedParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantValueParameter"/> class.
        /// </summary>
        /// <param name="match">The match predicate.</param>
        /// <param name="value">The value to resolve parameter to.</param>
        public ConstantValueParameter(Func<ParameterInfo, bool> match, object? value)
            : base(match, pi => value)
        {
        }
    }
}
