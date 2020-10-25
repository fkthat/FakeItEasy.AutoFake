using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace FakeItEasy.AutoFake
{
    public class AutoFaker : IAutoFaker
    {
        private readonly IDictionary<Type, object> _container = new Dictionary<Type, object>();

        public AutoFaker(Action<IAutoFakerConfiguration>? configure = null)
        {
            configure?.Invoke(new AutoFakerConfiguration(_container));
        }

        /// <summary>
        /// Creates the instance of the <paramref name="type"/> type.
        /// </summary>
        /// <param name="type">The type of an instance to create.</param>
        /// <returns>The created instance.</returns>
        public object CreateInstance(Type type, params IParameter[] parameters)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var ctors = type.GetConstructors();

            // validate uniqueness of constructor
            if (ctors.Length != 1)
            {
                throw new InvalidOperationException(
                    $"The type {type} has no or more than one public constructors.");
            }

            var values = new List<object>();

            foreach (var p in ctors[0].GetParameters())
            {
                var p1 = parameters.FirstOrDefault(x => x.Match(p));

                if (p1 != null)
                {
                    values.Add(p1.Resolve(p));
                    continue;
                }

                if (p.ParameterType.IsValueType)
                {
                    throw new InvalidOperationException(
                        $"One of the type {type} " +
                        "constructor parameters is not of a reference type.");
                }

                values.Add(Get(p.ParameterType));
            }

            return Activator.CreateInstance(type, values.ToArray());
        }

        /// <summary>
        /// Gets the service that will be provided by the AutoFake container. If the service of the
        /// <paramref name="type"/> type is provided by <see cref="IAutoFakerConfiguration.Use"/> it
        /// will return the instance provided. If a fake service of the <paramref name="type"/>
        /// wasn't created yet it will create and return a new fake.
        /// </summary>
        /// <param name="type">The type of a service.</param>
        /// <returns>The service instance.</returns>
        public object Get(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (type.IsValueType)
            {
                throw new ArgumentException(
                    $"The type {type} is not a class, interface, or delegate.", nameof(type));
            }

            if (!_container.TryGetValue(type, out object value))
            {
                value = Sdk.Create.Fake(type);
                _container.Add(type, value);
            }

            return value;
        }
    }
}
