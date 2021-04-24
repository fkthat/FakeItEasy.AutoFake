using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake
{
    public class Test_AutoFakerConfiguration
    {
        [Fact]
        public void Use_ShouldValidateInstance()
        {
            AutoFakerConfiguration testee = new();

            testee.Invoking(t => t.Use(typeof(int), "foo"))
                .Should().Throw<ArgumentException>().Which.ParamName.Should().Be("instance");

            testee.Invoking(t => t.Use(typeof(int), null))
                .Should().Throw<ArgumentException>().Which.ParamName.Should().Be("instance");

            testee.Invoking(t => t.Use(typeof(string), null)).Should().NotThrow();

            testee.Invoking(t => t.Use(typeof(int?), null)).Should().NotThrow();
        }

        [Fact]
        public void Use_ShouldAddPredefinedInstance()
        {
            AutoFakerConfiguration testee = new();
            var result = testee.Use(typeof(int), 42);
            result.Should().Be(testee);

            testee.PredefinedDependecies.Should().BeEquivalentTo(
                new Dictionary<Type, object?> { [typeof(int)] = 42 });
        }
    }
}
