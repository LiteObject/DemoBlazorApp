namespace DemoBlazorApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    ///     The table cell.
    /// </summary>
    public class TableCell
    {
        /// <summary>
        /// The value type.
        /// </summary>
        private Type valueType;

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the value type.
        /// </summary>
        [JsonIgnore]
        public Type ValueType
        {
            get => this.valueType;
            set
            {
                if (value != null)
                {
                    this.valueType = value;
                    // this.HtmlInputType = this.GetHtmlInputType(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the input attributes.
        /// </summary>
        public Dictionary<string, object> InputAttributes { get; set; } = new Dictionary<string, object>();
    }
}
