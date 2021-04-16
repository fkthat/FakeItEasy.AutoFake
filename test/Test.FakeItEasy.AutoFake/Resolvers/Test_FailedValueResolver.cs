using System.Reflection;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake.Resolvers
{
    public class Test_FailedValueResolver
    {
        [Fact]
        public void Resolve_ShouldReturnFailed()
        {
            var pi = A.Fake<ParameterInfo>();
            FailedValueResolver sut = new();
            var result = sut.Resolve(pi);
            result.Should().BeOfType<FailedResolvedValue>();
        }
    }
}
