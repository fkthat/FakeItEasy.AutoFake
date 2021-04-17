using System;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake
{
    public class Test_ConstantValueParameter
    {
        [Fact]
        public void Match_ShouldCallCtorPredicate()
        {
            var pi = A.Fake<ParameterInfo>();
            var match = A.Fake<Func<ParameterInfo, bool>>();
            A.CallTo(() => match(pi)).Returns(true);
            var sut = new ConstantValueParameter(match, 42);
            sut.Match(pi).Should().BeTrue();
        }

        [Fact]
        public void Resolve_ShouldUseCtorResolver()
        {
            var pi = A.Fake<ParameterInfo>();
            var match = A.Dummy<Func<ParameterInfo, bool>>();
            var sut = new ConstantValueParameter(match, 42);
            sut.GetValue(pi).Should().Be(42);
        }
    }
}
