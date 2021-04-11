using System;
using System.Collections.Generic;
using System.Reflection;
using FakeItEasy.AutoFake.Fakes;
using FakeItEasy.AutoFake.Parameters;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake.Resolvers
{
    public class Test_FakeValueResolver
    {
        public interface IFoo { }

        [Fact]
        public void Resolve_ShouldReturnValue()
        {
            var foo = A.Fake<IFoo>();
            var fakeFactory = A.Fake<IFakeFactory>();
            A.CallTo(() => fakeFactory.Get(typeof(IFoo))).Returns(foo);
            FakeValueResolver sut = new(fakeFactory, new FailedValueResolver());
            var pi = A.Fake<ParameterInfo>();
            A.CallTo(() => pi.ParameterType).Returns(typeof(IFoo));
            var r = sut.Resolve(pi);
            r.Should().BeOfType<ResolvedSuccessValue>().Which.Value.Should().Be(foo);
        }

        [Fact]
        public void Resolve_ShouldCallNext()
        {
            var fakeFactory = A.Fake<IFakeFactory>();
            A.CallTo(() => fakeFactory.Get(typeof(IFoo))).Returns(null);
            FakeValueResolver sut = new(fakeFactory, new FailedValueResolver());
            var pi = A.Fake<ParameterInfo>();
            A.CallTo(() => pi.ParameterType).Returns(typeof(IFoo));
            var r = sut.Resolve(pi);
            r.Should().BeOfType<ResolvedFailedValue>();
        }
    }
}
