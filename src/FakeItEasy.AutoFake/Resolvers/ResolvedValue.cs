namespace FakeItEasy.AutoFake.Resolvers
{
    /// <summary>
    /// A result of the value resolution.
    /// </summary>
    public abstract class ResolvedValue
    {
    }

    /// <summary>
    /// A result of a succesfull value resolution.
    /// </summary>
    public class SuccessResolvedValue : ResolvedValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SuccessResolvedValue"/> class.
        /// </summary>
        /// <param name="value">The resolved value.</param>
        public SuccessResolvedValue(object? value)
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
    public class FailedResolvedValue : ResolvedValue
    {
    }
}
