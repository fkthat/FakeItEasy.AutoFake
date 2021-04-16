using System;
using System.Collections.Generic;
using System.Reflection;
using FakeItEasy.AutoFake.Parameters;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake.Resolvers
{
    public class Test_PredefinedValueResolver
    {
        public interface IFoo { }

        [Fact]
        public void Resolve_ShouldReturnValue()
        {
            Dictionary<Type, object?> predefined = new() { [typeof(IFoo)] = "Bar" };
            PredefinedValueResolver sut = new(predefined, new FailedValueResolver());
            var pi = A.Fake<ParameterInfo>();
            A.CallTo(() => pi.ParameterType).Returns(typeof(IFoo));
            var r = sut.Resolve(pi);
            r.Should().BeOfType<SuccessResolvedValue>().Which.Value.Should().Be("Bar");
        }

        [Fact]
        public void Resolve_ShouldCallNext()
        {
            Dictionary<Type, object?> predefined = new() { };
            PredefinedValueResolver sut = new(predefined, new FailedValueResolver());
            var pi = A.Fake<ParameterInfo>();
            A.CallTo(() => pi.ParameterType).Returns(typeof(IFoo));
            var r = sut.Resolve(pi);
            r.Should().BeOfType<FailedResolvedValue>();
        }
    }
}
