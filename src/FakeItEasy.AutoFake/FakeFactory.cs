using System;

namespace FakeItEasy.AutoFake
{
    internal class FakeFactory : IFakeFactory
    {
        private readonly IAutoFakerConfiguration _configuration;

        public FakeFactory(IAutoFakerConfiguration configuration)
        {
            _configuration = configuration;
        }

        public object CreateFake(Type type) => Sdk.Create.Fake(type);
    }
}
