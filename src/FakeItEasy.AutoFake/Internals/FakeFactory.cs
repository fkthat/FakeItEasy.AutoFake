using System;
using FakeItEasy.Core;

namespace FakeItEasy.AutoFake.Internals
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
