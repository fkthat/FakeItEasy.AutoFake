using FakeItEasy.AutoFake.Fakes;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake
{
    public class Test_AutoFaker
    {
        public interface IFoo { }

        [Fact]
        public void Get_ShouldCallFakeFactory()
        {
            var foo = A.Fake<IFoo>();
            var fakeFactory = A.Fake<IFakeFactory>();
            A.CallTo(() => fakeFactory.Get(typeof(IFoo))).Returns(foo);
            AutoFaker sut = new(fakeFactory);
            var r = sut.Get(typeof(IFoo));
            r.Should().Be(foo);
        }
    }
}
