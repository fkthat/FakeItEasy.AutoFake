using System;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake.Parameters
{
    public class Test_IParameter
    {
        [Fact]
        public void TryResolve_ShouldReturnTrueAndValueOnSuccess()
        {
            var pi = A.Fake<ParameterInfo>();
            IParameter testee = A.Fake<P>();
            A.CallTo(() => testee.Match(pi)).Returns(true);
            A.CallTo(() => testee.Resolve(pi)).Returns(42);
            var result = testee.TryResolve(pi, out var value);
            result.Should().BeTrue();
            value.Should().Be(42);
        }

        [Fact]
        public void TryResolve_ShouldReturnTrueAndNullOnFailure()
        {
            var pi = A.Fake<ParameterInfo>();
            IParameter testee = A.Fake<P>();
            A.CallTo(() => testee.Match(pi)).Returns(false);
            A.CallTo(() => testee.Resolve(pi)).Throws<InvalidOperationException>();
            var result = testee.TryResolve(pi, out var value);
            result.Should().BeFalse();
            value.Should().BeNull();
        }

        public abstract class P : IParameter
        {
            public abstract bool Match(ParameterInfo parameterInfo);

            public abstract object? Resolve(ParameterInfo parameterInfo);
        }
    }
}
