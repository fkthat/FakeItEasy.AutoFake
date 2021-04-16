using System;

namespace FakeItEasy.AutoFake
{
    /// <summary>
    /// Fake factory.
    /// </summary>
    internal class FakeFactory : IFakeFactory
    {
        /// <summary>
        /// Creates a fake of the specified type.
        /// </summary>
        /// <param name="type">The type of the fake.</param>
        /// <returns>The new fake.</returns>
        public object CreateFake(Type type) => Sdk.Create.Fake(type);
    }
}
