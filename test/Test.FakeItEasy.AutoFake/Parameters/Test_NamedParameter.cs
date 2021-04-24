using System.Reflection;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake.Parameters
{
    public class Test_NamedParameter
    {
        [Fact]
        public void TryResolve_ShouldReturnTrueAndValueOnSuccess()
        {
            var pi = A.Fake<ParameterInfo>();
            A.CallTo(() => pi.Name).Returns("foo");
            NamedParameter testee = new("foo", 42);
            var result = testee.TryResolve(pi, out var value);
            result.Should().BeTrue();
            value.Should().Be(42);
        }

        [Fact]
        public void TryResolve_ShouldReturnFalseAndNullOnFailure()
        {
            var pi = A.Fake<ParameterInfo>();
            A.CallTo(() => pi.Name).Returns("foo");
            NamedParameter testee = new("bar", 42);
            var result = testee.TryResolve(pi, out var value);
            result.Should().BeFalse();
            value.Should().Be(null);
        }
    }
}
