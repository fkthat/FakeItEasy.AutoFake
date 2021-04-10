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
        public void CreateInstance_ShouldCallAutoFakerCreateInstance()
        {
            var expected = new object();
            var autoFaker = A.Fake<IAutoFaker>();
            A.CallTo(() => autoFaker.CreateInstance(typeof(object))).Returns(expected);
            var result = AutoFakerExtensions.CreateInstance<object>(autoFaker);
            result.Should().BeSameAs(expected);
        }
    }
}
