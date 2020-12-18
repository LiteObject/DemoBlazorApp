namespace DemoBlazorApp.Models
{
    using System;
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
                    this.HtmlInputType = this.GetHtmlInputType(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the html input type.
        /// </summary>
        public string HtmlInputType { get; set; } = "text";

        /// <summary>
        /// The get html input type.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetHtmlInputType(Type type)
        {
            string result;

            switch (type.Name.ToLower())
            {
                case "string":
                    result = "text";
                    break;
                case "int32":
                case "int64":
                case "decimal":
                case "double":
                    result = "number";
                    break;
                default:
                    result = "text";
                    break;
            }

            return result;
        }
    }
}
