using System.Reflection;

namespace FakeItEasy.AutoFake
{
    /// <summary>
    /// A parameter value resolver.
    /// </summary>
    internal interface IParameterResolver
    {
        /// <summary>
        /// Resolves a parameter value by the specified parameter information.
        /// </summary>
        /// <param name="parameterInfo">The parameter information.</param>
        ParameterValue GetValue(ParameterInfo parameterInfo);
    }
}
