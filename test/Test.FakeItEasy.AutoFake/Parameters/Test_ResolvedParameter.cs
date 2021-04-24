using System;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake.Parameters
{
    public class Test_ResolvedParameter
    {
        [Fact]
        public void TryResolve_ShouldReturnTrueAndValueOnSuccess()
        {
            var match = A.Fake<Func<ParameterInfo, bool>>();
            var resolve = A.Fake<Func<ParameterInfo, object>>();
            var pi = A.Fake<ParameterInfo>();
            A.CallTo(() => match(pi)).Returns(true);
            A.CallTo(() => resolve(pi)).Returns(42);
            var testee = new ResolvedParameter(match, resolve);
            var result = testee.TryResolve(pi, out var value);
            result.Should().BeTrue();
            value.Should().Be(42);
        }

        [Fact]
        public void TryResolve_ShouldReturnFalseAndNullOnFailure()
        {
            var match = A.Fake<Func<ParameterInfo, bool>>();
            var resolve = A.Fake<Func<ParameterInfo, object>>();
            var pi = A.Fake<ParameterInfo>();
            A.CallTo(() => match(pi)).Returns(false);
            A.CallTo(() => resolve(pi)).Throws<InvalidOperationException>();
            var testee = new ResolvedParameter(match, resolve);
            var result = testee.TryResolve(pi, out var value);
            result.Should().BeFalse();
            value.Should().Be(null);
        }
    }
}
