namespace DemoBlazorApp.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Models;
    using Models.TableTypes;

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
                        Id = 1,
                        Name = nameof(AddThreeNumbersModel),
                        Description = "Add three number",
                        Type = typeof(AddThreeNumbersModel)
                    });
            this.tables.Add(
                    new TableType {
                        Id = 2,
                        Name = nameof(AddFourNumbersModel),
                        Description = "Add Four Number",
                        Type = typeof(AddFourNumbersModel)
                    }
                );
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