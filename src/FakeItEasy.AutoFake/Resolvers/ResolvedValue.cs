namespace FakeItEasy.AutoFake.Resolvers
{
    internal abstract class ResolvedValue
    {
    }

    internal sealed class ResolvedSuccessValue : ResolvedValue
    {
        public ResolvedSuccessValue(object? value)
        {
            Value = value;
        }

        public object? Value { get; }
    }

    internal sealed class ResolvedFailedValue : ResolvedValue
    {
    }
}
