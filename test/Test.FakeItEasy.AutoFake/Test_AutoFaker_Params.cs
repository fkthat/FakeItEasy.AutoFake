using FakeItEasy.AutoFake.Parameters;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake
{
    public class Test_AutoFaker_Params
    {
        public interface IFoo { }

        public record Bar(IFoo Foo);

        [Fact]
        public void CreateInstance_ShouldUseParams()
        {
            var foo = A.Fake<IFoo>();
            AutoFaker sut = new();
            var result = sut.CreateInstance<Bar>(new TypedParameter<IFoo>(foo));
            result.Foo.Should().Be(foo);
        }
    }
}
