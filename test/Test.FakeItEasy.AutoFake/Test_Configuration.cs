using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake
{
    public class Test_Configuration
    {
        public interface IFoo { }

        [Fact]
        public void Use_ShouldValidateArguments()
        {
            var type = typeof(IFoo);
            Bar instance = new();
            Configuration sut = new();
            sut.Invoking(s => s.Use(type, instance))
                .Should().Throw<ArgumentException>().Which.ParamName
                .Should().Be("instance");
        }

        [Fact]
        public void Use_ShouldAddPredefinedInstance()
        {
            var type = typeof(IFoo);
            var instance = new Foo();
            Configuration sut = new();
            sut.Use(type, instance);
            sut.PredefinedInstances.Should().Contain(
                new KeyValuePair<Type, object?>(type, instance));
        }

        [Fact]
        public void Use_ShouldAcceptNull()
        {
            var type = typeof(IFoo);
            Foo? instance = null;
            Configuration sut = new();
            sut.Use(type, instance);
            sut.PredefinedInstances.Should().Contain(
                new KeyValuePair<Type, object?>(type, instance));
        }

        public class Foo : IFoo { }

        public class Bar { }
    }
}
