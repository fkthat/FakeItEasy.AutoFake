using System.Reflection;
using FakeItEasy.AutoFake.Parameters;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake.Resolvers
{
    public class Test_ParametersValueResolver
    {
        public interface IFoo { }

        [Fact]
        public void Resolve_ShouldReturnValue()
        {
            ParametersValueResolver sut = new(new FailedValueResolver());
            var pi = A.Fake<ParameterInfo>();
            A.CallTo(() => pi.ParameterType).Returns(typeof(IFoo));
            var r = sut.Resolve(pi, new TypedParameter<IFoo>("Bar"));
            r.Should().BeOfType<ResolvedSuccessValue>().Which.Value.Should().Be("Bar");
        }

        [Fact]
        public void Resolve_ShouldCallNext()
        {
            ParametersValueResolver sut = new(new FailedValueResolver());
            var pi = A.Fake<ParameterInfo>();
            A.CallTo(() => pi.ParameterType).Returns(typeof(IFoo));
            var r = sut.Resolve(pi);
            r.Should().BeOfType<ResolvedFailedValue>();
        }
    }
}
