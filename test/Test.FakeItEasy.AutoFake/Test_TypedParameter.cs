using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake
{
    public class Test_TypedParameter
    {
        [Fact]
        public void Match_ShouldMatchByType()
        {
            var pi = GetType().GetMethod("Foo")!.GetParameters()[0];
            var sut = new TypedParameter(typeof(int), 42);
            sut.Match(pi).Should().BeTrue();
            sut = new TypedParameter<int>(42);
            sut.Match(pi).Should().BeTrue();
        }

        [Fact]
        public void Resolve_ShouldReturnParameterValue()
        {
            var pi = GetType().GetMethod("Foo")!.GetParameters()[0];
            var sut = new TypedParameter(typeof(int), 42);
            sut.Resolve(pi).Should().Be(42);
            sut = new TypedParameter<int>(42);
            sut.Resolve(pi).Should().Be(42);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage",
            "xUnit1013:Public method should be marked as test",
            Justification = "Publicity required for tests")]
        public void Foo(int bar) { }
    }
}
