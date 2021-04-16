using System.Reflection;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake
{
    public class Test_TypedParameter
    {
        [Fact]
        public void Match_ShouldMatchByType()
        {
            var pi = A.Fake<ParameterInfo>();
            A.CallTo(() => pi.ParameterType).Returns(typeof(int));
            var sut = new TypedParameter(typeof(int), 42);
            sut.Match(pi).Should().BeTrue();
            sut = new TypedParameter<int>(42);
            sut.Match(pi).Should().BeTrue();
        }

        [Fact]
        public void Resolve_ShouldReturnParameterValue()
        {
            var pi = A.Fake<ParameterInfo>();
            A.CallTo(() => pi.ParameterType).Returns(typeof(int));
            var sut = new TypedParameter(typeof(int), 42);
            sut.GetValue(pi).Should().Be(42);
            sut = new TypedParameter<int>(42);
            sut.GetValue(pi).Should().Be(42);
        }
    }
}
