﻿namespace PhoneSystem.Common.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class IQueryableExtensions
    {
        public static DataTable ToDataTable<T>(this IQueryable items)
        {
            Type type = typeof(T);

            var props = TypeDescriptor
                .GetProperties(type)
                .Cast<PropertyDescriptor>()
                .Where(propertyInfo => propertyInfo.PropertyType.Namespace.Equals("System") || propertyInfo.PropertyType.Namespace.Equals("PhoneSystem.Models"))
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
