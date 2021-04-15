using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake
{
    public class Test_AutoFakerConfigurationExtensions
    {
        public interface IFoo { }

        [Fact]
        public void Use_ShouldCallConfiguration()
        {
            var foo = A.Fake<IFoo>();
            var configuration = A.Fake<IAutoFakerConfiguration>();
            A.CallTo(() => configuration.Use(typeof(IFoo), foo)).Returns(configuration);
            var r = configuration.Use(foo);
            A.CallTo(() => configuration.Use(typeof(IFoo), foo)).MustHaveHappened();
            r.Should().Be(configuration);
        }
    }
}
