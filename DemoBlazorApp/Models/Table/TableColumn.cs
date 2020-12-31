namespace DemoBlazorApp.Models
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    ///     The table column.
    /// </summary>
    public class TableColumn
    {
        /// <summary>
        ///     Gets or sets the index.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }


        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the data type.
        /// </summary>
        [JsonIgnore]
        public Type ValueType { get; set; }
    }
}
