using System;

namespace FakeItEasy.AutoFake.Parameters
{
    /// <summary>
    /// Matches a parameter by its type.
    /// </summary>
    public class TypedParameter : ResolvedParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypedParameter"/> class.
        /// </summary>
        /// <param name="type">Parameter type to match.</param>
        /// <param name="value">The value to resolve paramtet to.</param>
        public TypedParameter(Type type, object? value)
            : base(
                  pi => pi.ParameterType == type,
                  type.CanBe(value)
                    ? pi => value
                    : throw new ArgumentException("Invalid value type.", nameof(value)))
        {
        }
    }

    /// <summary>
    /// Matches a parameter by its type.
    /// </summary>
    /// <typeparam name="T">Parameter type to match.</typeparam>
    public class TypedParameter<T> : TypedParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypedParameter"/> class.
        /// </summary>
        /// <param name="value">The value to resolve paramtet to.</param>
        public TypedParameter(object value) : base(typeof(T), value)
        {
        }
    }
}
