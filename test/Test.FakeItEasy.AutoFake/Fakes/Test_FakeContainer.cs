using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake.Fakes
{
    public class Test_FakeContainer
    {
        public interface IFoo { }

        [Fact]
        public void Get_ShouldReturnFakeOnSuccess()
        {
            var fakeFactory = A.Fake<IFakeFactory>();
            A.CallTo(() => fakeFactory.Get(typeof(IFoo))).ReturnsLazily(_ => A.Fake<IFoo>());
            FakeContainer sut = new(fakeFactory);
            var result = sut.Get(typeof(IFoo));
            var result2 = sut.Get(typeof(IFoo));
            result.Should().BeAssignableTo<IFoo>();
            result2.Should().Be(result);
        }

        [Fact]
        public void Get_ShouldReturnNullOnFailure()
        {
            var fakeFactory = A.Fake<IFakeFactory>();
            A.CallTo(() => fakeFactory.Get(typeof(IFoo))).Returns(null);
            FakeContainer sut = new(fakeFactory);
            var result = sut.Get(typeof(IFoo));
            result.Should().BeNull();
        }
    }
}
