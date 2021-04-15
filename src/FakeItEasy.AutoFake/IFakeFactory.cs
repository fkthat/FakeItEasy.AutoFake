using System;

namespace FakeItEasy.AutoFake
{
    /// <summary>
    /// Fake factory.
    /// </summary>
    public interface IFakeFactory
    {
        /// <summary>
        /// Creates a fake of the specified type.
        /// </summary>
        /// <param name="type">The type of the fake.</param>
        /// <returns>The new fake.</returns>
        object CreateFake(Type type);
    }
}
