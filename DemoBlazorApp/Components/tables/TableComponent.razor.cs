namespace DemoBlazorApp.Components.tables
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Library;
    using Models;
    using Services;

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

        [Inject]
        public IMathService MathService { get; set; }

        [Inject]
        public IDynamicTableService DynamicTableService { get; set; }

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
            
            Util.Log("SelectedTableType: " + this.selectedTableType);

            // Target type
            Type tt = selectedTableType.Type;

            this.TableFactory.ScanForTables(Assembly.GetExecutingAssembly());

            // var model =  typeof(TargetMethodObject).GetMethod("TargetMethod").MakeGenericMethod(targetType).Invoke(null, new object[] { argument });
            // var x = this.TableFactory.GetType().GetMethod("Create").MakeGenericMethod(tt).Invoke(this.TableFactory,null);
            
            var model = Activator.CreateInstance(tt, MathService);

            this.DynamicTableService.SelectedTableType = this.SelectedTableType;
            this.table = this.DynamicTableService.GetDynamicTable(model);
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
            var updatedObject = this.DynamicTableService.ConvertTableRowToType(row);

            var index = this.table.Rows.IndexOf(row);

            if (index != -1)
            {
                Util.Log($"Name: {((BaseModel)updatedObject).Name}");
                this.table.Rows[index] = updatedObject.ToTableRow(index);
            }

            this.table.Rows.ForEach(r => r.Cells.ForEach(c => Util.Log("New Value " + c.Value)));
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
                            Util.Log($"Cell value is not in acceptable format: {cell.Value}");
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
                    Util.Log(e);
                }
            }

            return (T)obj;
        }

        /*private object ConvertTableRowToType(TableRow row, Type type)
        {
            var obj = Activator.CreateInstance(type, this.MathService);
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
                            Util.Log($"Cell value is not in acceptable format: {cell.Value}");
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
                    Util.Log(e);
                }
            }

            return obj;
        } */

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
                Util.Log($"Column Index: {i}, Name: {props[i].Name}, Type: {props[i].PropertyType.Name}");
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