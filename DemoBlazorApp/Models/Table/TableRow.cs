namespace DemoBlazorApp.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The table row.
    /// </summary>
    public class TableRow
    {
        /// <summary>
        /// Gets or sets the cells.
        /// </summary>
        public List<TableCell> Cells { get; set; } = new List<TableCell>();

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        public int Index { get; set; }
    }
}
