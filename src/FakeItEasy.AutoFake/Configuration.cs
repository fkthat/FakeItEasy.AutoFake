using System;
using System.Collections.Generic;
using System.Reflection;

namespace FakeItEasy.AutoFake
{
    internal class Configuration
    {
        private readonly Dictionary<Type, object?> _predefinedInstances = new();

        public IReadOnlyDictionary<Type, object?> PredefinedInstances => _predefinedInstances;

        public void Use(Type type, object? instance)
        {
            if (instance != null && !type.IsAssignableFrom(instance.GetType()))
            {
                throw new ArgumentException(
                    $"The {nameof(instance)} is not of the {type} type.",
                    nameof(instance));
            }

            _predefinedInstances.Add(type, instance);
        }
    }
}
