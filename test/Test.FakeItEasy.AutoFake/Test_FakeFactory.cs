using FakeItEasy.Core;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake
{
    public class Test_FakeFactory
    {
        public interface IFoo { }

        [Fact]
        public void CreateFake_ShouldReturnFakeForFakeableType()
        {
            var configuration = A.Fake<IAutoFakerConfiguration>();
            FakeFactory testee = new(configuration);
            var result = testee.CreateFake(typeof(IFoo));
            Fake.IsFake(result).Should().BeTrue();
            var fm = Fake.GetFakeManager(result);
            fm.FakeObjectType.Should().Be(typeof(IFoo));
        }

        [Fact]
        public void CreateFake_ShouldThrowForNotFakeableType()
        {
            var configuration = A.Fake<IAutoFakerConfiguration>();
            FakeFactory testee = new(configuration);

            testee.Invoking(t => t.CreateFake(typeof(int)))
                .Should().Throw<FakeCreationException>();
        }
    }
}
