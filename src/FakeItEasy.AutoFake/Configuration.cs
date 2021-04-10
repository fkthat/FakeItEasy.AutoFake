using System;
using System.Collections.Generic;

namespace FakeItEasy.AutoFake
{
    internal class Configuration
    {
        public IDictionary<Type, object> PredefinedInstances { get; init; } =
            new Dictionary<Type, object>();
    }
}
