namespace DemoBlazorApp.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DemoBlazorApp.Models;
    using DemoBlazorApp.Services;

    using Microsoft.AspNetCore.Components;

    /// <summary>
    /// The table.
    /// </summary>
    public partial class Table
    {
        /// <summary>
        /// Gets or sets the table service.
        /// </summary>
        [Inject]
        public ITableService TableService { get; set; }

        /// <summary>
        /// Gets or sets the table types.
        /// </summary>
        public List<TableType> TableTypes { get; set; } = new List<TableType>();

        /// <summary>
        /// Gets or sets the selected table type.
        /// </summary>
        public TableType SelectedTableType { get; set; } = new TableType();

        /// <summary>
        /// The on initialized async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var tableTypes = this.TableService.GetTableTypes();
            this.TableTypes.AddRange(tableTypes);
        }

        /// <summary>
        /// The on table selection.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        private void OnTableSelection(ChangeEventArgs obj)
        {
            try
            {
                // ToDo: use TryParse
                var id = obj.Value is null ? 0 : Convert.ToInt32(obj.Value);
                this.SelectedTableType = this.TableTypes.FirstOrDefault(t => t.Id == id);

                // ToDo: Throw exception
                if (this.SelectedTableType != null && this.SelectedTableType.Id == 0)
                {
                    Console.WriteLine($"Unable to set {nameof(this.SelectedTableType)} value.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }
        }
    }
}
