using System.Reflection;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake
{
    public class Test_MatchParameterResolver
    {
        [Fact]
        public void TryGetValue_ShouldReturnSuccessValueOnMatch()
        {
            var p1 = A.Fake<IParameter>();
            var p2 = A.Fake<IParameter>();
            var p3 = A.Fake<IParameter>();

            var pi = A.Fake<ParameterInfo>();

            A.CallTo(() => p1.Match(pi)).Returns(false);
            A.CallTo(() => p2.Match(pi)).Returns(true);
            A.CallTo(() => p2.GetValue(pi)).Returns(42);
            A.CallTo(() => p3.Match(pi)).Returns(false);

            var next = A.Fake<IParameterResolver>();
            MatchParameterResolver sut = new(next, new[] { p1, p2, p3 });

            var r = typeof(MatchParameterResolver)
                .GetMethod("TryGetValue", BindingFlags.NonPublic | BindingFlags.Instance)?
                .Invoke(sut, new[] { pi });

            r.Should().BeOfType<SuccessParameterValue>().Which.Value.Should().Be(42);
        }

        [Fact]
        public void TryGetValue_ShouldReturnNullOnNoMatch()
        {
            var p1 = A.Fake<IParameter>();
            var p2 = A.Fake<IParameter>();
            var p3 = A.Fake<IParameter>();

            var pi = A.Fake<ParameterInfo>();

            A.CallTo(() => p1.Match(pi)).Returns(false);
            A.CallTo(() => p2.Match(pi)).Returns(false);
            A.CallTo(() => p3.Match(pi)).Returns(false);

            var next = A.Fake<IParameterResolver>();
            MatchParameterResolver sut = new(next, new[] { p1, p2, p3 });

            var r = typeof(MatchParameterResolver)
                .GetMethod("TryGetValue", BindingFlags.NonPublic | BindingFlags.Instance)?
                .Invoke(sut, new[] { pi });

            r.Should().BeNull();
        }
    }
}
