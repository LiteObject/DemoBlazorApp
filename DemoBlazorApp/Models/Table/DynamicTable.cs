namespace DemoBlazorApp.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The my table.
    /// </summary>
    public class DynamicTable
    {
        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        public List<TableColumn> Columns { get; set; } = new List<TableColumn>();

        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        public List<TableRow> Rows { get; set; } = new List<TableRow>();
    }
}
