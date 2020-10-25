using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FakeItEasy.AutoFake
{
    /// <summary>
    /// <see cref="IParameter"/> implemanrtation that matches by type.
    /// </summary>
    public class TypedParameter : ConstantValueParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypedParameter"/> class.
        /// </summary>
        /// <param name="type">Parameter type to match.</param>
        /// <param name="value">The value to resolve paramtet to.</param>
        public TypedParameter(Type type, object? value)
            : base(pi => pi.ParameterType == type, value)
        {
        }
    }

    /// <summary>
    /// <see cref="IParameter"/> implemanrtation that matches by type.
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
