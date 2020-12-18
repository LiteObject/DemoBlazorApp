namespace DemoBlazorApp.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using DemoBlazorApp.Models;

    /// <summary>
    /// The table service.
    /// </summary>
    public class TableService : ITableService
    {
        /// <summary>
        /// The tables.
        /// </summary>
        private readonly List<TableType> tables = new List<TableType>();

        /// <summary>
        /// Initializes a new instance of the <see cref="TableService"/> class.
        /// </summary>
        public TableService()
        {
            this.tables.Add(
                new TableType
                    {
                        Id = 1, Name = nameof(TableOneModel), Description = "Table One", Type = typeof(TableOneModel)
                    });
            this.tables.Add(
                new TableType
                    {
                        Id = 2, Name = nameof(TableTwoModel), Description = "Table Two", Type = typeof(TableTwoModel)
                    });
            this.tables.Add(
                new TableType
                    {
                        Id = 3,
                        Name = nameof(TableMathModel),
                        Description = "Add three number",
                        Type = typeof(TableMathModel)
                    });
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <returns>
        /// The <see cref="IList{T}"/>.
        /// </returns>
        public IList<TableType> GetTableTypes()
        {
            return this.tables;
        }

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="TableType"/>.
        /// </returns>
        public TableType GetTableTypesById(int id)
        {
            return this.tables.FirstOrDefault(t => t.Id == id);
        }
    }
}