using FakeItEasy.Core;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake
{
    public class Test_FakeFactoryCachingDecorator
    {
        public interface IFoo { }

        [Fact]
        public void Get_ShouldReturnFakeOnSuccess()
        {
            var fakeFactory = A.Fake<IFakeFactory>();
            A.CallTo(() => fakeFactory.CreateFake(typeof(IFoo))).ReturnsLazily(_ => A.Fake<IFoo>());
            FakeFactoryCachingDecorator sut = new(fakeFactory);
            var result = sut.CreateFake(typeof(IFoo));
            var result2 = sut.CreateFake(typeof(IFoo));
            result.Should().BeAssignableTo<IFoo>();
            result2.Should().Be(result);
        }

        [Fact]
        public void Get_ShouldThrowFailure()
        {
            var fakeFactory = A.Fake<IFakeFactory>();
            A.CallTo(() => fakeFactory.CreateFake(typeof(IFoo))).Throws<FakeCreationException>();
            FakeFactoryCachingDecorator sut = new(fakeFactory);
            sut.Invoking(s => s.CreateFake(typeof(IFoo))).Should().Throw<FakeCreationException>();
        }
    }
}
