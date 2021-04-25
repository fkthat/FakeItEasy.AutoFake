using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake
{
    public class Test_FakeFactoryCacheDecorator
    {
        public interface IFoo { }

        [Fact]
        public void CreateFake_ShouldReturnNewFakeOnFirstCall()
        {
            var factory = A.Fake<IFakeFactory>();
            A.CallTo(() => factory.CreateFake(typeof(IFoo))).ReturnsLazily(c => A.Fake<IFoo>());
            FakeFactoryCacheDecorator testee = new(factory);
            var result = testee.CreateFake(typeof(IFoo));
            Fake.TryGetFakeManager(result, out var fm).Should().BeTrue();
            fm!.FakeObjectType.Should().Be(typeof(IFoo));
        }

        [Fact]
        public void CreateFake_ShouldReturnSameFakeOnSecondCall()
        {
            var factory = A.Fake<IFakeFactory>();
            A.CallTo(() => factory.CreateFake(typeof(IFoo))).ReturnsLazily(c => A.Fake<IFoo>());
            FakeFactoryCacheDecorator testee = new(factory);
            var result1 = testee.CreateFake(typeof(IFoo));
            var result2 = testee.CreateFake(typeof(IFoo));
            result2.Should().Be(result1);
        }
    }
}
