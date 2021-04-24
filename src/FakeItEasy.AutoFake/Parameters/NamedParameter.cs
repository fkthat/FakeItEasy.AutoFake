namespace FakeItEasy.AutoFake.Parameters
{
    /// <summary>
    /// Matches a parameter by its name.
    /// </summary>
    public class NamedParameter : ResolvedParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedParameter"/> class.
        /// </summary>
        /// <param name="name">The parameter name to match.</param>
        /// <param name="value">The value to resolve paramtet to.</param>
        public NamedParameter(string name, object? value)
            : base(pi => pi.Name == name, pi => value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Gets the matching parameter name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the matching parameter value.
        /// </summary>
        public object? Value { get; }
    }
}
