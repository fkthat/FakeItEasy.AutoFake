using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace FakeItEasy.AutoFake
{
    public class Test_AutoFakerConfigurationExtensions
    {
        [Fact]
        public void Use_ShouldCallConfiguration()
        {
            var configuration = A.Fake<IAutoFakerConfiguration>();
            var instance = new Foo();
            configuration.Use(instance);
            A.CallTo(() => configuration.Use(typeof(Foo), instance)).MustHaveHappened();
        }

        [Fact]
        public void Use_ShouldReturnConfiguration()
        {
            var configuration = A.Fake<IAutoFakerConfiguration>();
            var instance = new Foo();
            A.CallTo(() => configuration.Use(typeof(Foo), instance)).Returns(configuration);
            var result = configuration.Use(instance);
            result.Should().BeSameAs(configuration);
        }

        private class Foo
        {
        }
    }
}
