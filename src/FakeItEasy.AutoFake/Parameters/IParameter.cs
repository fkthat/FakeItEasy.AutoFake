using System.Reflection;

namespace FakeItEasy.AutoFake.Parameters
{
    /// <summary>
    /// Parameter value resolver.
    /// </summary>
    public interface IParameter
    {
        /// <summary>
        /// Tries to resolve parameter value.
        /// </summary>
        /// <param name="parameterInfo">The parameter information.</param>
        /// <param name="value">The resolved value on success, <c cref="false"/> otherwise.</param>
        /// <returns>
        /// <c cref="true"/> if the parameter successfully resolved, <c cref="false"/> otherwise.
        /// </returns>
        bool TryResolve(ParameterInfo parameterInfo, out object? value);
    }
}
