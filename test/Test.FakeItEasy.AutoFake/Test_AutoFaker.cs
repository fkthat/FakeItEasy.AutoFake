using System;
using FakeItEasy.AutoFake.Parameters;
using FakeItEasy.Core;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake
{
    public class Test_AutoFaker
    {
        public interface IFoo { }

        public interface IAutoFaked { }

        public interface IPredefined { }

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
        public void Get_ShouldCallFakeFactory()
        {
            var configuration = A.Dummy<IAutoFakerConfiguration>();
            var fakeFactory = A.Fake<IFakeFactory>();
            var foo = A.Fake<IFoo>();
            A.CallTo(() => fakeFactory.CreateFake(typeof(IFoo))).Returns(foo);
            AutoFaker testee = new(configuration, fakeFactory);
            var result = testee.Get<IFoo>();
            result.Should().Be(foo);
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
