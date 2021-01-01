namespace DemoBlazorApp.Services
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using Library;
    using Models;


    public class DynamicTableService : IDynamicTableService
    {
        public IMathService MathService { get; set; }

        public TableType SelectedTableType { get; set; }

        public DynamicTableService(IMathService mathService)
        {
            this.MathService = mathService ?? throw new ArgumentNullException(nameof(mathService));
            // this.SelectedTableType = selectedTableType ?? throw new ArgumentNullException(nameof(selectedTableType));
        }

        public DynamicTable GetDynamicTable(object model)
        {
            if (this.SelectedTableType is null)
            {
                Console.WriteLine($"{nameof(this.SelectedTableType)} is null. Please assign a value.");
            }

            var myTable = new DynamicTable();
            var props = SelectedTableType?.Type.GetSortedProperties().ToList();

            for (var i = 0; i < props?.Count; i++)
            {
                Util.Log($"Column Index: {i}, Name: {props[i].Name}, Type: {props[i].PropertyType.Name}");

                var description = props[i].GetCustomAttribute<DescriptionAttribute>()?.Description;

                myTable.Columns.Add(new TableColumn
                {
                    Index = i,
                    Name = props[i].Name,
                    Description = description,
                    ValueType = props[i].PropertyType
                }); ;
            }

            var row = model.ToTableRow(0);
            myTable.Rows.Add(row);

            return myTable;
        }

        public object ConvertTableRowToType(TableRow row)
        {
            var obj = Activator.CreateInstance(SelectedTableType.Type, this.MathService);
            var properties = SelectedTableType.Type.GetProperties();

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
                        if (cell.ColumnName.Equals("Total"))
                        {
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
    }
}
