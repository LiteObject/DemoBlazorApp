namespace DemoBlazorApp.Services
{
    using System.Collections.Generic;

    using DemoBlazorApp.Models;

    /// <summary>
    /// The TableService interface.
    /// </summary>
    public interface ITableService
    {
        /// <summary>
        /// The get.
        /// </summary>
        /// <returns>
        /// The <see cref="IList{T}"/>.
        /// </returns>
        IList<TableType> GetTableTypes();

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="TableType"/>.
        /// </returns>
        TableType GetTableTypesById(int id);
    }
}