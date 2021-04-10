using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake.Internals
{
    public class Test_FakeFactory
    {
        public interface IFoo { }

        [Fact]
        public void Get_ShouldReturnNewFakeOnSuccess()
        {
            FakeFactory sut = new();
            var result = sut.Get(typeof(IFoo));
            result.Should().BeAssignableTo<IFoo>();
        }

        [Fact]
        public void Get_ShouldReturnNullOnFailure()
        {
            FakeFactory sut = new();
            var result = sut.Get(typeof(int));
            result.Should().BeNull();
        }
    }
}
