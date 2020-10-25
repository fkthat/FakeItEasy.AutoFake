using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FakeItEasy;
using FakeItEasy.AutoFake;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake
{
    public class Test_ResolvedParameter
    {
        [Fact]
        public void Match_ShouldCallCtorPredicate()
        {
            var pi = GetType().GetMethod(nameof(Foo))!.GetParameters()[0];
            var match = A.Fake<Func<ParameterInfo, bool>>();
            var resolve = A.Dummy<Func<ParameterInfo, object?>>();
            A.CallTo(() => match(pi)).Returns(true);
            var sut = new ResolvedParameter(match, resolve);
            sut.Match(pi).Should().BeTrue();
        }

        [Fact]
        public void Resolve_ShouldUseCtorResolver()
        {
            var pi = GetType().GetMethod(nameof(Foo))!.GetParameters()[0];
            var match = A.Dummy<Func<ParameterInfo, bool>>();
            var resolve = A.Fake<Func<ParameterInfo, object?>>();
            A.CallTo(() => resolve(pi)).Returns(42);
            var sut = new ResolvedParameter(match, resolve);
            sut.Resolve(pi).Should().Be(42);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage",
            "xUnit1013:Public method should be marked as test", Justification = "<Pending>")]
        public void Foo(object foo) { }
    }
}
