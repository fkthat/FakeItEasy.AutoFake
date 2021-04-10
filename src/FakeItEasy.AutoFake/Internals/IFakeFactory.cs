using System;

namespace FakeItEasy.AutoFake.Internals
{
    internal interface IFakeFactory
    {
        object? Get(Type type);
    }
}
