using System.Reflection;
using FakeItEasy.Core;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake.Resolvers
{
    public class Test_FakeValueResolver
    {
        public interface IFoo { }

        [Fact]
        public void Resolve_ShouldReturnValue()
        {
            var foo = A.Fake<IFoo>();
            var fakeFactory = A.Fake<IFakeFactory>();
            A.CallTo(() => fakeFactory.CreateFake(typeof(IFoo))).Returns(foo);
            FakeValueResolver sut = new(fakeFactory, new FailedValueResolver());
            var pi = A.Fake<ParameterInfo>();
            A.CallTo(() => pi.ParameterType).Returns(typeof(IFoo));
            var r = sut.Resolve(pi);
            r.Should().BeOfType<SuccessResolvedValue>().Which.Value.Should().Be(foo);
        }

        [Fact]
        public void Resolve_ShouldCallNext()
        {
            var fakeFactory = A.Fake<IFakeFactory>();
            A.CallTo(() => fakeFactory.CreateFake(typeof(IFoo))).Throws<FakeCreationException>();
            FakeValueResolver sut = new(fakeFactory, new FailedValueResolver());
            var pi = A.Fake<ParameterInfo>();
            A.CallTo(() => pi.ParameterType).Returns(typeof(IFoo));
            var r = sut.Resolve(pi);
            r.Should().BeOfType<FailedResolvedValue>();
        }
    }
}
