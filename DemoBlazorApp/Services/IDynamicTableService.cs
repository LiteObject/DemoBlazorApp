using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoBlazorApp.Models;

namespace DemoBlazorApp.Services
{
    public interface IDynamicTableService
    {
        public IMathService MathService { get; set; }

        public TableType SelectedTableType { get; set; }

        DynamicTable GetDynamicTable(object model);

        object ConvertTableRowToType(TableRow row);
    }
}
