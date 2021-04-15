using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake
{
    public class Test_FakeFactory
    {
        public interface IFoo { }

        [Fact]
        public void Get_ShouldReturnNewFakeOnSuccess()
        {
            FakeFactory sut = new();
            var result = sut.CreateFake(typeof(IFoo));
            result.Should().BeAssignableTo<IFoo>();
        }

        [Fact]
        public void Get_ShouldThrowOnFailure()
        {
            FakeFactory sut = new();

            sut.Invoking(s => s.CreateFake(typeof(int))).Should()
                .Throw<Core.FakeCreationException>();
        }
    }
}
