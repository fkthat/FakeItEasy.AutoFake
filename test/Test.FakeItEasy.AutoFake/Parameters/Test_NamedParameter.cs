using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake
{
    public class Test_NamedParameter
    {
        [Fact]
        public void Match_ShouldMatchByName()
        {
            var pi = GetType().GetMethod(nameof(Foo))!.GetParameters()[0];
            var sut = new NamedParameter("bar", 42);
            sut.Match(pi).Should().BeTrue();
        }

        [Fact]
        public void Resolve_ShouldReturnParameterValue()
        {
            var pi = GetType().GetMethod(nameof(Foo))!.GetParameters()[0];
            var sut = new NamedParameter("bar", 42);
            sut.GetValue(pi).Should().Be(42);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage",
            "xUnit1013:Public method should be marked as test", Justification = "<Pending>")]
        public void Foo(int bar) { }
    }
}
