using System.Reflection;

namespace FakeItEasy.AutoFake.Parameters
{
    /// <summary>
    /// Parameter value resolver.
    /// </summary>
    public interface IParameter
    {
        /// <summary>
        /// Matches the specified parameter information.
        /// </summary>
        /// <param name="parameterInfo">The parameter information.</param>
        /// <returns>
        /// True if the value of specified parameter can be resolved, false otherwise.
        /// </returns>
        bool Match(ParameterInfo parameterInfo);

        /// <summary>
        /// Resolves value for the specified parameter.
        /// </summary>
        /// <param name="parameterInfo">The parameter information.</param>
        /// <returns>Resolved value. <br/></returns>
        object? Resolve(ParameterInfo parameterInfo);
    }
}
