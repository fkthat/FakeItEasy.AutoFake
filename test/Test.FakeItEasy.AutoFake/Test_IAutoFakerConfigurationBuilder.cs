using System;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake
{
    public class Test_IAutoFakerConfigurationBuilder
    {
        [Fact]
        public void UseGeneric_ShouldCallNonGeneric()
        {
            IAutoFakerConfigurationBuilder testee = A.Fake<C>();
            A.CallTo(() => testee.Use(typeof(int), 42)).Returns(testee);
            var result = testee.Use(42);
            result.Should().Be(testee);
            A.CallTo(() => testee.Use(typeof(int), 42)).MustHaveHappened();
        }

        public abstract class C : IAutoFakerConfigurationBuilder
        {
            public abstract IAutoFakerConfigurationBuilder Use(Type type, object instance);
        }
    }
}
