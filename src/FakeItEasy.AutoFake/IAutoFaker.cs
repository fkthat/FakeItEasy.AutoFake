using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeItEasy.AutoFake
{
    /// <summary>
    /// AutoMocker API.
    /// </summary>
    public interface IAutoFaker
    {
        /// <summary>
        /// Creates the instance of the <paramref name="type"/> type.
        /// </summary>
        /// <param name="type">The type of an instance to create.</param>
        /// <returns>The created instance.</returns>
        object CreateInstance(Type type, params IParameter[] parameters);
    }
}
