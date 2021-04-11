using System;
using System.Collections.Generic;
using System.Reflection;
using FakeItEasy.AutoFake.Parameters;

namespace FakeItEasy.AutoFake.Resolvers
{
    internal class PredefinedValueResolver : ChainValueResolver
    {
        private readonly IReadOnlyDictionary<Type, object?> _predefinedValues;

        public PredefinedValueResolver(IReadOnlyDictionary<Type, object?> predefinedValues,
            IValueResolver next) : base(next)
        {
            _predefinedValues = predefinedValues;
        }

        protected override ResolvedValue? TryResolve(ParameterInfo parameterInfo,
            params IParameter[] parameters) =>
            _predefinedValues.TryGetValue(parameterInfo.ParameterType, out var v)
            ? new ResolvedSuccessValue(v) : null;
    }
}
