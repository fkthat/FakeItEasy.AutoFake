using System;
using FakeItEasy.Core;

namespace FakeItEasy.AutoFake.Fakes
{
    internal class FakeFactory : IFakeFactory
    {
        public object Get(Type type) => Sdk.Create.Fake(type);
    }
}
