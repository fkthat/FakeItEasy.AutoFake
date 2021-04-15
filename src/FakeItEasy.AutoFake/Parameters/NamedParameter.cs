namespace FakeItEasy.AutoFake.Parameters
{
    /// <summary>
    /// <see cref="IParameter"/> implementation that matches by the parameter name.
    /// </summary>
    public class NamedParameter : ConstantValueParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedParameter"/> class.
        /// </summary>
        /// <param name="name">Parameter name to match.</param>
        /// <param name="value">The value to resolve paramtet to.</param>
        public NamedParameter(string name, object? value)
            : base(pi => pi.Name == name, value)
        {
        }
    }
}
