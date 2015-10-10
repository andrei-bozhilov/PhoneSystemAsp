namespace PhoneSystem.Common.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Reflection;

    public static class IQueryableExtensions
    {
        public static DataTable ToDataTable(this IQueryable items)
        {
            Type type = items.ElementType;

            var props = TypeDescriptor
                .GetProperties(type)
                .Cast<PropertyDescriptor>()
                .Where(propertyInfo => propertyInfo.PropertyType.Namespace.Equals("System")
                        || propertyInfo.PropertyType.Namespace.Equals("PhoneSystem.Models")
                        || propertyInfo.PropertyType.Namespace.Equals("PhoneSystem.Web"))
                .Where(propertyInfo => propertyInfo.IsReadOnly == false)
                .ToArray();

            var table = new DataTable();

            foreach (var propertyInfo in props)
            {
                table.Columns.Add(propertyInfo.Name, Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType);
            }

            foreach (var item in items)
            {
                table.Rows.Add(props.Select(property => property.GetValue(item)).ToArray());
            }

            return table;
        }
    }
}
