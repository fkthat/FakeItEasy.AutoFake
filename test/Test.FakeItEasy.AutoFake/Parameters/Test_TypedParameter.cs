using System;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake.Parameters
{
    public class Test_TypedParameter
    {
        [Fact]
        public void Ctor_ShouldValidateValueType()
        {
            FluentActions.Invoking(() => new TypedParameter(typeof(int), "foo"))
                .Should().Throw<ArgumentException>().Which.ParamName.Should().Be("value");

            FluentActions.Invoking(() => new TypedParameter(typeof(int), null))
                .Should().Throw<ArgumentException>().Which.ParamName.Should().Be("value");

            FluentActions.Invoking(() => new TypedParameter(typeof(string), null))
                .Should().NotThrow();

            FluentActions.Invoking(() => new TypedParameter(typeof(int?), null))
                .Should().NotThrow();
        }

        [Fact]
        public void TryResolve_ShouldReturnTrueAndValueOnSuccess()
        {
            var pi = A.Fake<ParameterInfo>();
            A.CallTo(() => pi.ParameterType).Returns(typeof(int));
            TypedParameter testee = new(typeof(int), 42);
            var result = testee.TryResolve(pi, out var value);
            result.Should().BeTrue();
            value.Should().Be(42);
        }

        [Fact]
        public void TryResolve_ShouldReturnFalseAndNullOnFailure()
        {
            var pi = A.Fake<ParameterInfo>();
            A.CallTo(() => pi.ParameterType).Returns(typeof(string));
            TypedParameter testee = new(typeof(int), 42);
            var result = testee.TryResolve(pi, out var value);
            result.Should().BeFalse();
            value.Should().Be(null);
        }

        [Fact]
        public void TryResolveGeneric_ShouldReturnTrueAndValueOnSuccess()
        {
            var pi = A.Fake<ParameterInfo>();
            A.CallTo(() => pi.ParameterType).Returns(typeof(int));
            TypedParameter<int> testee = new(42);
            var result = testee.TryResolve(pi, out var value);
            result.Should().BeTrue();
            value.Should().Be(42);
        }

        [Fact]
        public void TryResolveGeneric_ShouldReturnFalseAndNullOnFailure()
        {
            var pi = A.Fake<ParameterInfo>();
            A.CallTo(() => pi.ParameterType).Returns(typeof(string));
            TypedParameter<int> testee = new(42);
            var result = testee.TryResolve(pi, out var value);
            result.Should().BeFalse();
            value.Should().Be(null);
        }
    }
}
