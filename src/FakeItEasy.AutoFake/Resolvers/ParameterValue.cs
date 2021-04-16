namespace FakeItEasy.AutoFake.Resolvers
{
    /// <summary>
    /// A result of the value resolution.
    /// </summary>
    public abstract class ParameterValue
    {
    }

    /// <summary>
    /// A result of a succesfull value resolution.
    /// </summary>
    public class SuccessParameterValue : ParameterValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SuccessParameterValue"/> class.
        /// </summary>
        /// <param name="value">The resolved value.</param>
        public SuccessParameterValue(object? value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public object? Value { get; }
    }

    /// <summary>
    /// An unsuccessfull value resolution result.
    /// </summary>
    public class FailedParameterValue : ParameterValue
    {
    }
}
