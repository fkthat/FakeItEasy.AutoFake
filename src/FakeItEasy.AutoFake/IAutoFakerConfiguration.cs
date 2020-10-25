using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace FakeItEasy.AutoFake
{
    public interface IAutoFakerConfiguration
    {
        /// <summary>
        /// Provide a predefined instance of a service to inject later to created objects.
        /// </summary>
        /// <param name="type">The type of a service.</param>
        /// <param name="instance">The instance of a service.</param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="type"/> or the <paramref name="instance"/> is null.
        /// </exception>
        IAutoFakerConfiguration Use(Type type, object instance);
    }
}
