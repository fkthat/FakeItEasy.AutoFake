using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake
{
    public class Test_AutoFakerExtensions
    {
        public interface IFoo { }

        [Fact]
        public void CreateInstance_ShouldCallAutoFaker()
        {
            var foo = new Foo();
            var autoFaker = A.Fake<IAutoFaker>();
            A.CallTo(() => autoFaker.CreateInstance(typeof(Foo))).Returns(foo);
            var r = autoFaker.CreateInstance<Foo>();
            A.CallTo(() => autoFaker.CreateInstance(typeof(Foo))).MustHaveHappened();
            r.Should().Be(foo);
        }

        [Fact]
        public void Get_ShouldCallAutoFaker()
        {
            var foo = A.Fake<IFoo>();
            var autoFaker = A.Fake<IAutoFaker>();
            A.CallTo(() => autoFaker.Get(typeof(IFoo))).Returns(foo);
            var r = autoFaker.Get<IFoo>();
            A.CallTo(() => autoFaker.Get(typeof(IFoo))).MustHaveHappened();
            r.Should().Be(foo);
        }

        public class Foo : IFoo { }
    }
}
