using System;

namespace FakeItEasy.AutoFake.Fakes
{
    internal interface IFakeFactory
    {
        object? Get(Type type);
    }
}
