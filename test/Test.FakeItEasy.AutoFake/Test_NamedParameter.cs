using System.Reflection;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake
{
    public class Test_NamedParameter
    {
        [Fact]
        public void Match_ShouldMatchByName()
        {
            var pi = A.Fake<ParameterInfo>();
            A.CallTo(() => pi.Name).Returns("bar");
            var sut = new NamedParameter("bar", 42);
            sut.Match(pi).Should().BeTrue();
        }

        [Fact]
        public void Resolve_ShouldReturnParameterValue()
        {
            var pi = A.Fake<ParameterInfo>();
            A.CallTo(() => pi.Name).Returns("bar");
            var sut = new NamedParameter("bar", 42);
            sut.GetValue(pi).Should().Be(42);
        }
    }
}
