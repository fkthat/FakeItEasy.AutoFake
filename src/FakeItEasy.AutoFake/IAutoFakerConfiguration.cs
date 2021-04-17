using System;
using System.Collections.Generic;

namespace FakeItEasy.AutoFake
{
    internal interface IAutoFakerConfiguration
    {
        IReadOnlyDictionary<Type, object?> PredefinedInstances { get; }
    }
}
