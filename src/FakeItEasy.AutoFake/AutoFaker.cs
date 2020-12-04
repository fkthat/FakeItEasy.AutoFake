using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using FakeItEasy.Core;

namespace FakeItEasy.AutoFake
{
    public class AutoFaker : IAutoFaker
    {
        private readonly IDictionary<Type, object> _container = new Dictionary<Type, object>();
        private readonly IDictionary<Type, object> _predefined = new Dictionary<Type, object>();

        public AutoFaker(Action<IAutoFakerConfiguration>? configure = null)
        {
            configure?.Invoke(new AutoFakerConfiguration(_predefined));
        }

        /// <summary>
        /// Creates the instance of the <paramref name="type"/> type.
        /// </summary>
        /// <param name="type">The type of an instance to create.</param>
        /// <returns>The created instance.</returns>
        public object CreateInstance(Type type, params IParameter[] parameters)
        {
            object?[]? values = null;

            foreach (var ctor in type.GetConstructors())
            {
                try
                {
                    var values2 = Resolve(ctor.GetParameters(), parameters);

                    if (values == null || values.Length < values2.Length)
                    {
                        values = values2;
                    }
                }
                catch (FakeCreationException)
                {
                    continue;
                }
            }

            if (values != null)
            {
                return Activator.CreateInstance(type, values)!;
            }

            throw new InvalidOperationException(
                $"No suitable constructor to create an instance of {type}");
        }

        /// <summary>
        /// Gets the service that will be provided by the AutoFake container. If a fake service of
        /// the <paramref name="type"/> wasn't created yet it will create and return a new fake.
        /// </summary>
        /// <param name="type">The type of a service.</param>
        /// <returns>The service instance.</returns>
        public object Get(Type type)
        {
            if (_predefined.ContainsKey(type))
            {
                throw new ArgumentException($"{type} is configured as predefined dependency.",
                    nameof(type));
            }

            if (!_container.TryGetValue(type, out object? value))
            {
                value = Sdk.Create.Fake(type);
                _container.Add(type, value!);
            }

            return value;
        }

        private object?[] Resolve(ParameterInfo[] pis, IParameter[] parameters) =>
            pis.Select(p => Resolve(p, parameters)).ToArray();

        private object? Resolve(ParameterInfo pi, IParameter[] parameters)
        {
            var p = parameters.FirstOrDefault(x => x.Match(pi));

            if (p != null)
            {
                return p.Resolve(pi);
            }

            if (_predefined.ContainsKey(pi.ParameterType))
            {
                return _predefined[pi.ParameterType];
            }

            return Get(pi.ParameterType);
        }
    }
}
