namespace FakeItEasy.AutoFake.Resolvers
{
    internal abstract record ResolvedValue();
    internal sealed record ResolvedSuccessValue(object? Value) : ResolvedValue;
    internal sealed record ResolvedFailedValue() : ResolvedValue;
}
