using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake
{
    public class Test_AutoFaker_Get
    {
        public interface IFoo { }

        [Fact]
        public void Get_ShouldCreateFake()
        {
            var sut = new AutoFaker();
            var result = sut.Get(typeof(IFoo));
            var result2 = sut.Get(typeof(IFoo));
            result.Should().BeAssignableTo<IFoo>();
            result2.Should().Be(result);
        }

        [Fact]
        public void GetT_ShouldCreateFake()
        {
            var sut = new AutoFaker();
            var result = sut.Get<IFoo>();
            var result2 = sut.Get<IFoo>();
            result.Should().BeAssignableTo<IFoo>();
            result2.Should().Be(result);
        }
    }
}
