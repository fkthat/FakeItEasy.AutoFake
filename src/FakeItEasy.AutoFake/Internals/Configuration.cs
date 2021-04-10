using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeItEasy.AutoFake.Internals
{
    internal class Configuration
    {
        public IDictionary<Type, object> PredefinedInstances { get; init; } =
            new Dictionary<Type, object>();
    }
}
