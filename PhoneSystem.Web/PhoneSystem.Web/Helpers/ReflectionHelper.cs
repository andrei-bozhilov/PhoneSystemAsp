namespace PhoneSystem.Web.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class ReflectionHelper
    {
        public static IList<Type> GetSubClasses<T>()
        {

            if (typeof(T).IsGenericType)
            {
                return Assembly.GetCallingAssembly()
                  .GetTypes()
                  .Where(type => type.IsSubclassOfRawGeneric(typeof(T)) && !type.IsAbstract)
                  .ToList();
            }
            else
            {
                return Assembly.GetCallingAssembly()
                   .GetTypes()
                   .Where(type => type.IsSubclassOf(typeof(T)) && !type.IsAbstract)
                   .Where(type => type.BaseType == typeof(T))
                   .ToList();
            }
        }

        public static bool IsSubclassOfRawGeneric(this Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }
    }
}