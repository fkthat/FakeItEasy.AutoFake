using System.Reflection;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake
{
    public class Test_FailedParameterResolver
    {
        [Fact]
        public void Resolve_ShouldReturnFailed()
        {
            var pi = A.Fake<ParameterInfo>();
            FailedParameterResolver sut = new();
            var result = sut.GetValue(pi);
            result.Should().BeOfType<FailedParameterValue>();
        }
    }
}
