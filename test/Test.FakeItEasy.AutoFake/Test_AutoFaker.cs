using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeItEasy.Core;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace FakeItEasy.AutoFake
{
    public class Test_AutoFaker
    {
        public interface IAutoFaked
        {
        }

        public interface IPredefined
        {
        }

        [Fact]
        public void CreateInstance_ShouldReturnNewInstance()
        {
            var predefined = A.Fake<IPredefined>();
            var sut = new AutoFaker(configure => configure.Use(predefined));
            var result = sut.CreateInstance<Foo>(new TypedParameter<int>(42));
            result.AutoFaked.Should().BeAssignableTo<IAutoFaked>();
            result.Predefined.Should().Be(predefined);
            result.Bar.Should().Be(42);
        }

        [Fact]
        public void CreateInstance_WithNoCtor_ShouldThrow()
        {
            var sut = new AutoFaker();
            sut.Invoking(s => s.CreateInstance<Bad>())
                .Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Fake_WithNonFakeableType_ShouldThrowFakeCreationException()
        {
            FluentActions.Invoking(() => Sdk.Create.Fake(typeof(int)))
                .Should().Throw<FakeCreationException>();
        }

        public class Foo
        {
            // not selected
            public Foo()
            {
            }

            // not selected
            public Foo(IAutoFaked autoFaked, IPredefined predefined)
            {
            }

            // not selected
            public Foo(IAutoFaked autoFaked, IPredefined predefined, string bar)
            {
            }

            // this one will be selected
            public Foo(IAutoFaked autoFaked, IPredefined predefined, int bar)
            {
                AutoFaked = autoFaked;
                Predefined = predefined;
                Bar = bar;
            }

            public IAutoFaked? AutoFaked { get; }

            public IPredefined? Predefined { get; }

            public int Bar { get; }
        }

        // no suitable ctor
        public class Bad
        {
            protected Bad()
            {
            }
        }
    }
}
