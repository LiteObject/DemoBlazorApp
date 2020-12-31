namespace DemoBlazorApp.Library
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Dynamic;
    using System.Linq;
    using System.Reflection;
    using System.Text.Json;

    using DemoBlazorApp.Models;

    using MoreLinq;

    /// <summary>
    /// The extensions.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// The convert to type.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <typeparam name="T">
        /// The type.
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T ConvertToType<T>(this object value) where T : class
        {
            var jsonData = JsonSerializer.Serialize(value);
            return JsonSerializer.Deserialize<T>(jsonData);
        }

        /// <summary>
        /// The to dynamic.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="dynamic"/>.
        /// </returns>
        public static dynamic ToDynamic(this object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
            {
                expando.Add(property.Name, property.GetValue(value));
            }

            return (ExpandoObject)expando;
        }

        /// <summary>
        /// The get prop value.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public static object GetPropValue(this object obj, string name)
        {
            foreach (string part in name.Split('.'))
            {
                if (obj == null)
                {
                    return null;
                }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);

                if (info == null)
                {
                    return null;
                }

                obj = info.GetValue(obj, null);
            }

            return obj;
        }

        /// <summary>
        /// The get prop value.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T GetPropValue<T>(this object obj, string name)
        {
            object retval = GetPropValue(obj, name);

            if (retval == null)
            {
                return default(T);
            }

            // throws InvalidCastException if types are incompatible
            return (T)retval;
        }

        /// <summary>
        /// The to table cells.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <returns>
        /// The <see cref="IList{T}"/>.
        /// </returns>
        public static List<TableCell> ToTableCells(this object obj, int rowIndex = 0)
        {
            List<TableCell> cells = new List<TableCell>();

            // var props = obj.GetType().GetProperties();
            var props = obj.GetType().GetSortedProperties().ToList();
            
            for (var j = 0; j < props.Count; j++)
            {
                var prop = props[j];
                var rawAttributes = prop.GetCustomAttributes<HtmlInputAttribute>()?.ToList();

                var inputAttributes = new Dictionary<string, object>();
                
                if (rawAttributes.Any() == false)
                {
                    Console.WriteLine("No input attributes found");
                }
                else
                {
                    var uniqueAttributes = rawAttributes.DistinctBy(a => a.Key);
                    inputAttributes = uniqueAttributes.ToDictionary(a => a.Key, a => (object)a.Value);
                }

                var keyAttr = prop.GetCustomAttribute<KeyAttribute>();
                
                var cellValue = GetPropValue(obj, prop.Name)?.ToString();

                if (keyAttr != null) {
                    cellValue = rowIndex.ToString();
                }

                var cell = new TableCell
                               {
                                   Index = j,
                                   ColumnName = prop?.Name,
                                   Value = cellValue,
                                   ValueType = prop?.PropertyType,
                                   InputAttributes = inputAttributes ?? new Dictionary<string, object>()
                };

                cells.Add(cell);
            }

            return cells;
        }

        /// <summary>
        /// The to table row.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="TableRow"/>.
        /// </returns>
        public static TableRow ToTableRow(this object obj, int index)
        {
            TableRow row = new TableRow { Index = index, Cells = obj.ToTableCells(index) };
            return row;
        }

        /// <summary>
        /// The get sorted properties.
        /// </summary>
        /// <param name="t">
        /// The t.
        /// </param>
        /// <returns>
        /// The <see cref="IOrderedEnumerable{T}"/>.
        /// </returns>
        public static IOrderedEnumerable<PropertyInfo> GetSortedProperties(this Type t)
        {
            // ToDo: Handle props without "OrderAttribute"
            return t
                .GetProperties()
                .OrderBy(p => ((OrderAttribute)p.GetCustomAttributes(typeof(OrderAttribute), true)[0]).Order);
        }

        /* public static IOrderedEnumerable<PropertyInfo> GetSortedProperties<T>()
        {
            return typeof(T)
                .GetProperties()
                .OrderBy(p => ((OrderAttribute)p.GetCustomAttributes(typeof(OrderAttribute), false)[0]).Order);
        } */
    }
}
