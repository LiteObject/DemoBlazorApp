namespace DemoBlazorApp.Models
{
    using DemoBlazorApp.Library;

    /// <summary>
    /// The table two model.
    /// </summary>
    public class TableTwoModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the column two.
        /// </summary>
        [Order]
        public string Phone { get; set; } = "000-000-0000";

        /// <summary>
        /// Gets or sets the note.
        /// </summary>
        [Order]
        public string Note { get; set; } = "N/A";
    }
}
