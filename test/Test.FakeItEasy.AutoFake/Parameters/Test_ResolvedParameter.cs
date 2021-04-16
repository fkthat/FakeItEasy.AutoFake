using System;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake
{
    public class Test_ResolvedParameter
    {
        [Fact]
        public void Match_ShouldCallCtorPredicate()
        {
            var pi = A.Fake<ParameterInfo>();
            var match = A.Fake<Func<ParameterInfo, bool>>();
            var resolve = A.Dummy<Func<ParameterInfo, object?>>();
            A.CallTo(() => match(pi)).Returns(true);
            var sut = new ResolvedParameter(match, resolve);
            sut.Match(pi).Should().BeTrue();
        }

        [Fact]
        public void Resolve_ShouldUseCtorResolver()
        {
            var pi = A.Fake<ParameterInfo>();
            var match = A.Dummy<Func<ParameterInfo, bool>>();
            var resolve = A.Fake<Func<ParameterInfo, object?>>();
            A.CallTo(() => resolve(pi)).Returns(42);
            var sut = new ResolvedParameter(match, resolve);
            sut.GetValue(pi).Should().Be(42);
        }
    }
}
