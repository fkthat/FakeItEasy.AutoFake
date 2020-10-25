using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Xunit;
using Xunit.Sdk;

namespace FakeItEasy.AutoFake
{
    public class Test_AutoFakerConfiguration
    {
        public interface IFoo
        {
        }

        [Fact]
        public void Use_ShouldValidateArguments()
        {
            var container = new Dictionary<Type, object>();
            var sut = new AutoFakerConfiguration(container);

            sut.Invoking(s => s.Use(type: null, instance: A.Fake<IFoo>()))
                .Should().Throw<ArgumentNullException>().Which.ParamName
                .Should().Be("type");

            sut.Invoking(s => s.Use(type: typeof(IFoo), instance: null))
                .Should().Throw<ArgumentNullException>().Which.ParamName
                .Should().Be("instance");

            // instance is not an instance of type
            sut.Invoking(s => s.Use(type: typeof(IFoo), instance: "Bar"))
                .Should().Throw<ArgumentException>().Which.ParamName
                .Should().Be("instance");
        }

        [Fact]
        public void Use_ShouldAddPredefinedService()
        {
            var container = new Dictionary<Type, object>();
            var foo = A.Fake<IFoo>();
            var sut = new AutoFakerConfiguration(container);
            sut.Use(typeof(IFoo), foo);
            container.Should().ContainKey(typeof(IFoo))
                .WhichValue.Should().BeSameAs(foo);
        }

        [Fact]
        public void Use_ShouldReturnInstanceItself()
        {
            var container = new Dictionary<Type, object>();
            var foo = A.Fake<IFoo>();
            var sut = new AutoFakerConfiguration(container);
            var result = sut.Use(typeof(IFoo), foo);
            result.Should().BeSameAs(sut);
        }
    }
}
