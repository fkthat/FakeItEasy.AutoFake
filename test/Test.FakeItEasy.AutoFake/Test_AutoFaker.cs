using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public void CreateInstance_ShouldValidateArguments()
        {
            var sut = new AutoFaker();

            sut.Invoking(s => s.CreateInstance(null))
                .Should().Throw<ArgumentNullException>().Which
                .ParamName.Should().Be("type");

            sut.Invoking(s => s.CreateInstance(typeof(Bad0)))
                .Should().Throw<InvalidOperationException>();

            sut.Invoking(s => s.CreateInstance(typeof(Bad1)))
                .Should().Throw<InvalidOperationException>();

            sut.Invoking(s => s.CreateInstance(typeof(Bad2)))
                .Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void CreateInstance_ShouldReturnNewInstance()
        {
            var predefined = A.Fake<IPredefined>();
            var sut = new AutoFaker(config => config.Use(predefined));
            var result = sut.CreateInstance(typeof(Foo));
            var ofTypeFoo = result.Should().BeOfType<Foo>().Which;
            ofTypeFoo.AutoFaked.Should().BeSameAs(sut.Get(typeof(IAutoFaked)));
            ofTypeFoo.Predefined.Should().BeSameAs(predefined);
        }

        [Fact]
        public void Get_ShouldValidateArguments()
        {
            var sut = new AutoFaker();

            sut.Invoking(s => s.Get(null)).Should().Throw<ArgumentNullException>().Which
                .ParamName.Should().Be("type");

            sut.Invoking(s => s.Get(typeof(int))).Should().Throw<ArgumentException>().Which
                .ParamName.Should().Be("type");
        }

        [Fact]
        public void Get_ShouldCreateAndReturnFake()
        {
            var sut = new AutoFaker();
            var result = sut.Get(typeof(IAutoFaked));
            result.Should().BeAssignableTo<IAutoFaked>();
        }

        [Fact]
        public void Get_ShouldReturnCachedFake()
        {
            var sut = new AutoFaker();
            var result1 = sut.Get(typeof(IAutoFaked));
            var result2 = sut.Get(typeof(IAutoFaked));
            result2.Should().BeSameAs(result1);
        }

        [Fact]
        public void Get_ShouldReturnPredefinedFake()
        {
            var predefined = A.Fake<IPredefined>();
            var sut = new AutoFaker(config => config.Use(predefined));
            var result = sut.Get(typeof(IPredefined));
            result.Should().BeSameAs(predefined);
        }

        public class Foo
        {
            public Foo(IAutoFaked autoFaked, IPredefined predefined)
            {
                AutoFaked = autoFaked;
                Predefined = predefined;
            }

            public IAutoFaked AutoFaked { get; }

            public IPredefined Predefined { get; }
        }

        // no public ctor
        public class Bad0
        {
            protected Bad0()
            {
            }
        }

        // more than one public ctors
        public class Bad1
        {
            public Bad1()
            {
            }

            public Bad1(IAutoFaked autoFaked)
            {
            }
        }

        // ctor with not-reference args
        public class Bad2
        {
            public Bad2(IAutoFaked autoFaked, int arg)
            {
            }
        }
    }
}
