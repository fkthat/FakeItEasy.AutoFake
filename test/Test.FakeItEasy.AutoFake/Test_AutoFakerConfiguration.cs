using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake
{
    public class Test_AutoFakerConfiguration
    {
        public interface IFoo { }

        [Fact]
        public void Use_ShouldValidateArguments()
        {
            var type = typeof(IFoo);
            Bar instance = new();
            AutoFakerConfiguration sut = new();
            sut.Invoking(s => s.Use(type, instance))
                .Should().Throw<ArgumentException>().Which.ParamName
                .Should().Be("instance");
        }

        [Fact]
        public void Use_ShouldAddPredefinedInstance()
        {
            var type = typeof(IFoo);
            var instance = new Foo();
            AutoFakerConfiguration sut = new();
            var r = sut.Use(type, instance);
            sut.PredefinedInstances.Should().Contain(
                new KeyValuePair<Type, object?>(type, instance));
            r.Should().Be(sut);
        }

        [Fact]
        public void Use_ShouldAcceptNull()
        {
            var type = typeof(IFoo);
            Foo? instance = null;
            AutoFakerConfiguration sut = new();
            var r = sut.Use(type, instance);
            sut.PredefinedInstances.Should().Contain(
                new KeyValuePair<Type, object?>(type, instance));
            r.Should().Be(sut);
        }

        public class Foo : IFoo { }

        public class Bar { }
    }
}
