using System.Reflection;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake.Resolvers
{
    public class Test_ChainParameterResolver
    {
        [Fact]
        public void GetValue_ShouldReturnSuccessValueOnTryGetValueNotNull()
        {
            var sut = A.Fake<ChainParameterResolver>();
            var pi = A.Fake<ParameterInfo>();

            A.CallTo(sut).Where(c =>
                c.Method.Name == "TryGetValue" &&
                c.Arguments[0] == pi)
                .WithReturnType<ParameterValue>()
                .Returns(new SuccessParameterValue(42));

            var r = sut.GetValue(pi);

            r.Should().BeOfType<SuccessParameterValue>().Which.Value.Should().Be(42);
        }

        [Fact]
        public void GetValue_ShouldCallNextOnTryGetValueNull()
        {
            var next = A.Fake<IParameterResolver>();

            var sut = A.Fake<ChainParameterResolver>(options =>
                options.WithArgumentsForConstructor(new[] { next }));

            var pi = A.Fake<ParameterInfo>();

            A.CallTo(() => next.GetValue(pi)).Returns(new SuccessParameterValue(42));

            A.CallTo(sut).Where(c =>
                c.Method.Name == "TryGetValue" &&
                c.Arguments[0] == pi)
                .WithReturnType<ParameterValue?>()
                .Returns(null);

            var r = sut.GetValue(pi);

            r.Should().BeOfType<SuccessParameterValue>().Which.Value.Should().Be(42);
        }
    }
}
