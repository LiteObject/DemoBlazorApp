namespace DemoBlazorApp.Components.tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http.Headers;
    using System.Reflection;
    using System.Text.Json;

    using DemoBlazorApp.Library;
    using DemoBlazorApp.Models;

    using Microsoft.AspNetCore.Components;

    /// <summary>
    ///     The table.
    /// </summary>
    public partial class TableComponent
    {
        /// <summary>
        /// The my table.
        /// </summary>
        private DynamicTable table = null;

        /// <summary>
        ///     The selected table.
        /// </summary>
        private TableType selectedTableType;

        /// <summary>
        ///     Gets or sets the selected table.
        /// </summary>
        [Parameter]
        public TableType SelectedTableType
        {
            get => this.selectedTableType;

            set
            {
                this.selectedTableType = value;
                this.DisplayTable();
            }
        }

        [Inject]
        public ITableFactory TableFactory { get; set; }

        /// <summary>
        /// The get prop value.
        /// </summary>
        /// <param name="src">
        /// The source.
        /// </param>
        /// <param name="propName">
        /// The prop name.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        private object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName)?.GetValue(src, null);
        }

        /// <summary>
        /// The display table.
        /// </summary>
        private void DisplayTable()
        {
            if (this.selectedTableType is null)
            {
                return;
            }

            Console.WriteLine("Display Table: " + this.selectedTableType);

            // Target type
            Type tt = selectedTableType.Type;

            this.TableFactory.ScanForTables(Assembly.GetExecutingAssembly());

            // var model =  typeof(TargetMethodObject).GetMethod("TargetMethod").MakeGenericMethod(targetType).Invoke(null, new object[] { argument });
            // var x = this.TableFactory.GetType().GetMethod("Create").MakeGenericMethod(tt).Invoke(this.TableFactory,null);
            
            var model = Activator.CreateInstance(tt);
            this.table = this.GetDynamicTable(model);

        }

        private DynamicTable GetDynamicTable(object model)
        {
            var myTable = new DynamicTable();
            var props = this.selectedTableType.Type.GetSortedProperties().ToList();

            for (var i = 0; i < props.Count; i++)
            {
                Console.WriteLine($"Column Index: {i}, Name: {props[i].Name}, Type: {props[i].PropertyType.Name}");

                var description = props[i].GetCustomAttribute<DescriptionAttribute>()?.Description;

                myTable.Columns.Add(new TableColumn {
                    Index = i,
                    Name = props[i].Name,
                    Description = description ?? string.Empty,
                    ValueType = props[i].PropertyType
                }); ;
            }

            var row = model.ToTableRow(0);
            myTable.Rows.Add(row);

            return myTable;
        }
        
        /// <summary>
        /// The on row change.
        /// </summary>
        /// <param name="row">
        /// The row.
        /// </param>
        private void OnRowChange(TableRow row)
        {
            var obj = this.table.Rows.FirstOrDefault(r => r.Index == row.Index);
            var updatedObject = this.ConvertTableRowToType(row, this.selectedTableType.Type);

            var index = this.table.Rows.IndexOf(row);

            if (index != -1)
            {
                Console.WriteLine($"Name: {((BaseModel)updatedObject).Name}");
                this.table.Rows[index] = updatedObject.ToTableRow(index);
            }

            this.table.Rows.ForEach(r => r.Cells.ForEach(c => Console.WriteLine("New Value " + c.Value)));
        }

        /// <summary>
        /// The convert table row to type.
        /// </summary>
        /// <param name="row">
        /// The row.
        /// </param>
        /// <typeparam name="T">
        /// The type.
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        private T ConvertTableRowToType<T>(TableRow row)
        {
            var type = typeof(T);
            var obj = Activator.CreateInstance(type);
            var properties = type.GetProperties();

            foreach (var propertyInfo in properties)
            {
                try
                {
                    var cell = row.Cells.FirstOrDefault(c => c.ColumnName == propertyInfo.Name);

                    if (cell != null)
                    {
                        // ToDo: Need input validation.
                        if (cell.ValueType.Name.StartsWith("Int")  && (decimal.TryParse(cell.Value, out var d) == false || d > 100))
                        {
                            Console.WriteLine($"Cell value is not in acceptable format: {cell.Value}");
                            continue;
                        }
                        
                        // var cellValue = cell.ValueType.Name.StartsWith("Int") && cell.Value.Trim() == string.Empty ? "0" : cell.Value.Trim();
                        var newValue = Convert.ChangeType(cell.Value, propertyInfo.PropertyType);
                        propertyInfo.SetValue(obj, newValue, null);
                    }
                }
                catch (Exception e)
                {
                    // ToDo: try/catch to handle error: "System.FormatException: Input string was not in a correct format."
                    Console.WriteLine(e);
                }
            }

            return (T)obj;
        }

        private object ConvertTableRowToType(TableRow row, Type type)
        {
            var obj = Activator.CreateInstance(type);
            var properties = type.GetProperties();

            foreach (var propertyInfo in properties)
            {
                try
                {
                    var cell = row.Cells.FirstOrDefault(c => c.ColumnName == propertyInfo.Name);

                    if (cell != null)
                    {
                        // ToDo: Need input validation.
                        if (cell.ValueType.Name.StartsWith("Int") && (decimal.TryParse(cell.Value, out var d) == false || d > 100))
                        {
                            Console.WriteLine($"Cell value is not in acceptable format: {cell.Value}");
                            continue;
                        }

                        // ToDo: Find a better way
                        if (cell.ColumnName.Equals("Total")) {
                            continue;
                        }

                        // var cellValue = cell.ValueType.Name.StartsWith("Int") && cell.Value.Trim() == string.Empty ? "0" : cell.Value.Trim();
                        var newValue = Convert.ChangeType(cell.Value, propertyInfo.PropertyType);
                        propertyInfo.SetValue(obj, newValue, null);
                    }
                }
                catch (Exception e)
                {
                    // ToDo: try/catch to handle error: "System.FormatException: Input string was not in a correct format."
                    Console.WriteLine(e);
                }
            }

            return obj;
        }

        /// <summary>
        /// The add row.
        /// </summary>
        private void AddRow()
        {
            var newIndex = this.table.Rows.Count;
            var obj = Activator.CreateInstance(this.selectedTableType.Type);

            var newRow = obj?.ToTableRow(newIndex);
            this.table.Rows.Add(newRow);
        }

        /// <summary>
        /// The remove row.
        /// </summary>
        /// <param name="row">
        /// The row.
        /// </param>
        private void RemoveRow(TableRow row)
        {
            this.table.Rows.Remove(row);
        }

        private DynamicTable GetDynamicTable<T>(List<T> items)
        {
            var myTable = new DynamicTable();

            // var props = typeof(T).GetProperties().ToList();
            var props = typeof(T).GetProperties().ToList();

            for (var i = 0; i < props.Count; i++)
            {
                Console.WriteLine($"Column Index: {i}, Name: {props[i].Name}, Type: {props[i].PropertyType.Name}");
                myTable.Columns.Add(new TableColumn { Index = i, Name = props[i].Name, ValueType = props[i].PropertyType });
            }

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var row = item.ToTableRow(i);
                myTable.Rows.Add(row);
            }

            return myTable;
        }
    }
}