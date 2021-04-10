using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake
{
    public class Test_AutoFaker_Use
    {
        public interface IFoo { }

        public record Bar(IFoo Foo);

        [Fact]
        public void Use_ShouldValidateArguments()
        {
            var sut = new AutoFaker();

            // instance is not an instance of type
            sut.Invoking(s => s.Use(type: typeof(IFoo), instance: "Bar"))
                .Should().Throw<ArgumentException>().Which.ParamName
                .Should().Be("instance");
        }

        [Fact]
        public void Use_ShouldReturnSelf()
        {
            var foo = A.Fake<IFoo>();
            var sut = new AutoFaker();
            var result = sut.Use(typeof(IFoo), foo);
            result.Should().Be(sut);
        }

        [Fact]
        public void UseT_ShouldReturnSelf()
        {
            var foo = A.Fake<IFoo>();
            var sut = new AutoFaker();
            var result = sut.Use(foo);
            result.Should().Be(sut);
        }

        [Fact]
        public void Use_ShouldSetPredefinedInstance()
        {
            var foo = A.Fake<IFoo>();
            var sut = new AutoFaker();
            sut.Use(foo);
            var result = sut.CreateInstance<Bar>();
            result.Foo.Should().Be(foo);
        }
    }
}
