using System;
using FakeItEasy.Core;

namespace FakeItEasy.AutoFake.Fakes
{
    internal class FakeFactory : IFakeFactory
    {
        public object? Get(Type type)
        {
            try
            {
                return Sdk.Create.Fake(type);
            }
            catch (FakeCreationException)
            {
                return null;
            }
        }
    }
}
