using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake
{
    public class Test_AutoFakerExtensions
    {
        [Fact]
        public void CreateInstance_ShouldValidateArguments()
        {
            FluentActions.Invoking(() => AutoFakerExtensions.CreateInstance<object>(null))
                .Should().Throw<ArgumentNullException>().Which.ParamName
                .Should().Be("autoFaker");
        }

        [Fact]
        public void CreateInstance_ShouldCallAutoFakerCreateInstance()
        {
            var expected = new object();
            var autoFaker = A.Fake<IAutoFaker>();
            A.CallTo(() => autoFaker.CreateInstance(typeof(object))).Returns(expected);
            var result = AutoFakerExtensions.CreateInstance<object>(autoFaker);
            result.Should().BeSameAs(expected);
        }

        [Fact]
        public void Get_ShouldValidateArguments()
        {
            FluentActions.Invoking(() => AutoFakerExtensions.Get<object>(null))
                .Should().Throw<ArgumentNullException>().Which.ParamName
                .Should().Be("autoFaker");
        }

        [Fact]
        public void Get_ShouldCallAutoFakerGet()
        {
            var expected = new object();
            var autoFaker = A.Fake<IAutoFaker>();
            A.CallTo(() => autoFaker.Get(typeof(object))).Returns(expected);
            var result = AutoFakerExtensions.Get<object>(autoFaker);
            result.Should().BeSameAs(expected);
        }
    }
}
