using System;
using System.Collections.Generic;

namespace FakeItEasy.AutoFake
{
    internal class AutoFakerConfiguration : IAutoFakerConfiguration, IAutoFakerConfigurationBuilder
    {
        private readonly Dictionary<Type, object?> _predefinedDepenedecies = new();

        public IReadOnlyDictionary<Type, object?> PredefinedDependecies => _predefinedDepenedecies;

        public IAutoFakerConfigurationBuilder Use(Type type, object? instance)
        {
            if (!type.CanBe(instance))
            {
                throw new ArgumentException("Invalid instance type.", nameof(instance));
            }

            _predefinedDepenedecies.Add(type, instance);
            return this;
        }
    }
}
