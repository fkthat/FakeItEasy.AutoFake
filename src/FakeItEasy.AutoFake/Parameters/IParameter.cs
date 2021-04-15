using System.Reflection;

namespace FakeItEasy.AutoFake.Parameters
{
    /// <summary>
    /// A parameter to pass additionaly to the <c cref="AutoFaker.CreateInstance"/>.
    /// </summary>
    public interface IParameter
    {
        /// <summary>
        /// Determines if the the value of the specified parameter can be provided by this instance
        /// of <c cref="IParameter"/>.
        /// </summary>
        /// <param name="parameterInfo">The parameter information.</param>
        bool Match(ParameterInfo parameterInfo);

        /// <summary>
        /// Gets the value for the specified parameter.
        /// </summary>
        /// <param name="parameterInfo">The parameter information.</param>
        object? GetValue(ParameterInfo parameterInfo);
    }
}
