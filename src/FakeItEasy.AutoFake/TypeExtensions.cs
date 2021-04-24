using System;

namespace FakeItEasy.AutoFake
{
    internal static class TypeExtensions
    {
        public static bool CanBe(this Type type, object? value) =>
            value is not null && type.IsAssignableFrom(value.GetType()) ||
                // value is null
                type.IsClass ||
                type.IsInterface ||
                type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
    }
}
