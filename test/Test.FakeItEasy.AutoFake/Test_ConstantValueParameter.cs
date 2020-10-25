using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake
{
    public class Test_ConstantValueParameter
    {
        [Fact]
        public void Match_ShouldCallCtorPredicate()
        {
            var pi = GetType().GetMethod("Foo").GetParameters()[0];
            var match = A.Fake<Func<ParameterInfo, bool>>();
            A.CallTo(() => match(pi)).Returns(true);
            var sut = new ConstantValueParameter(match, 42);
            sut.Match(pi).Should().BeTrue();
        }

        [Fact]
        public void Resolve_ShouldUseCtorResolver()
        {
            var pi = GetType().GetMethod("Foo").GetParameters()[0];
            var match = A.Dummy<Func<ParameterInfo, bool>>();
            var sut = new ConstantValueParameter(match, 42);
            sut.Resolve(pi).Should().Be(42);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage",
            "xUnit1013:Public method should be marked as test", Justification = "<Pending>")]
        public void Foo(object foo) { }
    }
}
