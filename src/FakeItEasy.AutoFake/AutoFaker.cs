using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FakeItEasy.AutoFake.Parameters;
using FakeItEasy.Core;

namespace FakeItEasy.AutoFake
{
    /// <summary>
    /// An auto-mocking IoC container that generates fake objects using FakeItEasy.
    /// </summary>
    public class AutoFaker
    {
        private readonly IDictionary<Type, object> _container = new Dictionary<Type, object>();
        private readonly IDictionary<Type, object> _predefined = new Dictionary<Type, object>();

        /// <summary>
        /// Provide a predefined instance of a service to inject later to created objects.
        /// </summary>
        /// <param name="type">The service type.</param>
        /// <param name="instance">The service instance.</param>
        public AutoFaker Use(Type type, object instance)
        {
            if (!type.IsAssignableFrom(instance.GetType()))
            {
                throw new ArgumentException(
                    $"The {nameof(instance)} is not of the {type} type.",
                    nameof(instance));
            }

            _predefined.Add(type, instance);
            return this;
        }

        /// <summary>
        /// Provide a predefined instance of a service to inject later to created objects.
        /// </summary>
        /// <typeparam name="T">The service type.</typeparam>
        /// <param name="instance">The service instance.</param>
        public AutoFaker Use<T>(T instance) where T : class => Use(typeof(T), instance);

        /// <summary>
        /// Creates the instance of the <paramref name="type"/> type.
        /// </summary>
        /// <param name="type">The type of an instance to create.</param>
        /// <param name="parameters">
        /// The parameters to pass to the <paramref name="type"/> constructor.
        /// </param>
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
                return Activator.CreateInstance(type, values);
            }

            throw new InvalidOperationException(
                $"No suitable constructor to create an instance of {type}");
        }

        /// <summary>
        /// Creates the instance of the <typeparamref name="T"/> type.
        /// </summary>
        /// <param name="parameters">
        /// The parameters to pass to the <typeparamref name="T"/> constructor.
        /// </param>
        /// <returns>The created instance.</returns>

        public T CreateInstance<T>(params IParameter[] parameters) =>
            (T)CreateInstance(typeof(T), parameters);

        /// <summary>
        /// Gets the service that will be provided by the AutoFake container. If a fake service of
        /// the <paramref name="type"/> wasn't created yet it will create and return a new fake.
        /// </summary>
        /// <param name="type">The type of a service.</param>
        /// <returns>The service instance.</returns>
        public object Get(Type type)
        {
            if (!_container.TryGetValue(type, out object value))
            {
                value = Sdk.Create.Fake(type);
                _container.Add(type, value);
            }

            return value;
        }

        /// <summary>
        /// Gets the service that will be provided by the AutoFake container. If a fake service of
        /// the <typeparamref name="T"/> wasn't created yet it will create and return a new fake.
        /// </summary>
        /// <returns>The service instance.</returns>
        public T Get<T>() where T : class => (T)Get(typeof(T));

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
