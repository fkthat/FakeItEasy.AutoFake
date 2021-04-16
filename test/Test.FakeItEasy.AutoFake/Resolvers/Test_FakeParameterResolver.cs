using System.Reflection;
using FakeItEasy.Core;
using FluentAssertions;
using Xunit;

namespace FakeItEasy.AutoFake.Resolvers
{
    public class Test_FakeParameterResolver
    {
        public interface IFoo { }

        [Fact]
        public void TryGetValue_ShouldReturnSuccessValueOnFake()
        {
            var foo = A.Fake<IFoo>();
            var factory = A.Fake<IFakeFactory>();
            var pi = A.Fake<ParameterInfo>();
            var next = A.Fake<IParameterResolver>();

            A.CallTo(() => pi.ParameterType).Returns(typeof(IFoo));
            A.CallTo(() => factory.CreateFake(typeof(IFoo))).Returns(foo);

            FakeParameterResolver sut = new(next, factory);

            var r = sut.GetType()
                .GetMethod("TryGetValue", BindingFlags.NonPublic | BindingFlags.Instance)?
                .Invoke(sut, new[] { pi });

            r.Should().BeOfType<SuccessParameterValue>().Which.Value.Should().Be(foo);
        }

        [Fact]
        public void TryGetValue_ShouldReturnNullOnFailure()
        {
            var foo = A.Fake<IFoo>();
            var factory = A.Fake<IFakeFactory>();
            var pi = A.Fake<ParameterInfo>();
            var next = A.Fake<IParameterResolver>();

            A.CallTo(() => pi.ParameterType).Returns(typeof(IFoo));
            A.CallTo(() => factory.CreateFake(typeof(IFoo))).Throws<FakeCreationException>();

            FakeParameterResolver sut = new(next, factory);

            var r = sut.GetType()
                .GetMethod("TryGetValue", BindingFlags.NonPublic | BindingFlags.Instance)?
                .Invoke(sut, new[] { pi });

            r.Should().BeNull();
        }
    }
}
