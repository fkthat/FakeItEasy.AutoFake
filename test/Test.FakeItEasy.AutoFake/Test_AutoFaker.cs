using System;
using FakeItEasy.AutoFake.Parameters;
using FakeItEasy.Core;
using FluentAssertions;
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
            AutoFaker testee = new(configure => configure.Use(predefined));
            var result = testee.CreateInstance<Foo>(new TypedParameter<int>(42));
            result.AutoFaked.Should().BeAssignableTo<IAutoFaked>();
            result.Predefined.Should().Be(predefined);
            result.Bar.Should().Be(42);
        }

        [Fact]
        public void CreateInstance_WithNoCtor_ShouldThrow()
        {
            AutoFaker testee = new();
            testee.Invoking(s => s.CreateInstance<Bad>())
                .Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Get_ShouldCreateAndReturnFake()
        {
            var sut = new AutoFaker();
            var result = sut.Get<IAutoFaked>();
            result.Should().BeAssignableTo<IAutoFaked>();
        }

        [Fact]
        public void Get_ShouldReturnCachedFake()
        {
            AutoFaker testee = new();
            var result1 = testee.Get<IAutoFaked>();
            var result2 = testee.Get<IAutoFaked>();
            result2.Should().BeSameAs(result1);
        }

        [Fact]
        public void Get_ShouldThrowWithPredefined()
        {
            var predefined = A.Fake<IPredefined>();
            AutoFaker testee = new(config => config.Use(predefined));
            testee.Invoking(s => s.Get<IPredefined>()).Should().Throw<ArgumentException>();
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
