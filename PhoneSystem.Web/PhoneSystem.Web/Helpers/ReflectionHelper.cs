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

        public static void SetValue<T>(T model, string propertyName, string value)
        {
            PropertyInfo propertyInfo = model.GetType().GetProperty(propertyName);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(model, Converts(value, propertyInfo), null);
            }
        }

        public static object Converts(string value, PropertyInfo propertyInfo)
        {
            Type type = propertyInfo.PropertyType;

            //handle nullable types
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                if (string.IsNullOrEmpty(value))
                {
                    return null;
                }
                else
                {
                    return Convert.ChangeType(value, type.GetGenericArguments()[0]);
                }
            }
            //handle enums
            if (propertyInfo.PropertyType.IsEnum)
            {
                return Enum.Parse(propertyInfo.PropertyType, value);
            }

            return Convert.ChangeType(value, propertyInfo.PropertyType);
        }

        public static object CreateObjectOf(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}