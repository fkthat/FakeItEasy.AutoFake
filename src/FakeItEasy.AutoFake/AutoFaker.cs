using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FakeItEasy.Core;

namespace FakeItEasy.AutoFake
{
    public class AutoFaker
    {
        private readonly IDictionary<Type, object> _container = new Dictionary<Type, object>();
        private readonly IAutoFakerConfiguration _configuration;

        public AutoFaker(Action<IAutoFakerConfigurationBuilder>? configure = null)
            : this(CreateConfiguration(configure))
        {
        }

        internal AutoFaker(IAutoFakerConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Creates the instance of the <paramref name="type"/> type.
        /// </summary>
        /// <param name="type">The type of an instance to create.</param>
        /// <returns>The created instance.</returns>
        public object CreateInstance(Type type, params Parameters.IParameter[] parameters)
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
                return Activator.CreateInstance(type, values);
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
            if (_configuration.PredefinedDependecies.ContainsKey(type))
            {
                throw new ArgumentException($"{type} is configured as a predefined dependency.",
                    nameof(type));
            }

            if (!_container.TryGetValue(type, out object value))
            {
                value = Sdk.Create.Fake(type);
                _container.Add(type, value);
            }

            return value;
        }

        /// <summary>
        /// Creates the instance of the <typeparamref name="T"/> type.
        /// </summary>
        /// <returns>The created instance.</returns>
        public T CreateInstance<T>(params Parameters.IParameter[] parameters)
            => (T)CreateInstance(typeof(T), parameters);

        /// <summary>
        /// Gets the service that will be provided by the AutoFake container. If the service of the
        /// <typeparamref name="T"/> type is provided by <see cref="IAutoMockerConfiguration.Use"/>
        /// it will return the instance provided. If a fake service of the <typeparamref name="T"/>
        /// wasn't created yet it will create and return a new fake.
        /// </summary>
        /// <returns>The service instance.</returns>
        public T Get<T>() where T : class => (T)Get(typeof(T));

        internal static IAutoFakerConfiguration CreateConfiguration(
                                            Action<IAutoFakerConfigurationBuilder>? configure)
        {
            var configuration = new AutoFakerConfiguration();
            configure?.Invoke(configuration);
            return configuration;
        }

        private object?[] Resolve(ParameterInfo[] pis, Parameters.IParameter[] parameters) =>
                            pis.Select(p => Resolve(p, parameters)).ToArray();

        private object? Resolve(ParameterInfo pi, Parameters.IParameter[] parameters)
        {
            foreach (var parameter in parameters)
            {
                if (parameter.TryResolve(pi, out var v1))
                {
                    return v1;
                }
            }

            if (_configuration.PredefinedDependecies.TryGetValue(pi.ParameterType, out var v2))
            {
                return v2;
            }

            return Get(pi.ParameterType);
        }
    }
}
