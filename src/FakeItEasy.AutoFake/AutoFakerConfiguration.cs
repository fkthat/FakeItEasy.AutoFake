using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FakeItEasy.AutoFake
{
    internal class AutoFakerConfiguration : IAutoFakerConfiguration
    {
        private readonly IDictionary<Type, object> _container;

        public AutoFakerConfiguration(IDictionary<Type, object> container)
        {
            _container = container;
        }

        public IAutoFakerConfiguration Use(Type type, object instance)
        {
            if (!type.IsAssignableFrom(instance.GetType()))
            {
                throw new ArgumentException(
                    $"The {nameof(instance)} is not of the {type} type.",
                    nameof(instance));
            }

            _container.Add(type, instance);
            return this;
        }
    }
}
