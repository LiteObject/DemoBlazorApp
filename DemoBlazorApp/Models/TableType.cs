namespace DemoBlazorApp.Models
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The table type.
    /// </summary>
    public class TableType
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [JsonIgnore]
        public Type Type { get; set; }
    }
}