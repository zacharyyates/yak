using System;
using System.Linq;

namespace Yak {
    public static class TypeExtensions {
        public static bool IsNonGenericImplementationOf(this Type type, Type interfaceType) {
            type.ThrowIfNull("type");
            interfaceType.ThrowIfNull("interfaceType");

            return type.GetInterfaces().Any(interfaceType.Equals);
        }

        public static bool IsGenericImplementationOf(this Type type, Type interfaceType) {
            type.ThrowIfNull("type");
            interfaceType.ThrowIfNull("interfaceType");

            return type.GetInterfaces().Any(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == interfaceType);
        }

        public static bool IsImplementationOf(this Type type, Type interfaceType) {
            type.ThrowIfNull("type");
            interfaceType.ThrowIfNull("interfaceType");

            return
                IsNonGenericImplementationOf(type, interfaceType) ||
                IsGenericImplementationOf(type, interfaceType);
        }
    }
}