using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake
{
    public class Test_IAutoFakerConfigurationBuilder
    {
        public interface IFoo { }

        [Fact]
        public void Use_ShouldCallNonGeneric()
        {
            var foo = A.Fake<IFoo>();
            IAutoFakerConfigurationBuilder builder = A.Fake<Builder>();
            A.CallTo(() => builder.Use(typeof(IFoo), foo)).Returns(builder);
            var r = builder.Use(foo);
            r.Should().Be(builder);
            A.CallTo(() => builder.Use(typeof(IFoo), foo)).MustHaveHappened();
        }

        public abstract class Builder : IAutoFakerConfigurationBuilder
        {
            public abstract IAutoFakerConfigurationBuilder Use(Type type, object? instance);
        }
    }
}
