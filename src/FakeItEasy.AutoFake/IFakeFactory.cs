using System;

namespace FakeItEasy.AutoFake
{
    internal interface IFakeFactory
    {
        object CreateFake(Type type);
    }
}
