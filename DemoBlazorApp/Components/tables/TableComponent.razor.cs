﻿namespace DemoBlazorApp.Components.tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;

    using DemoBlazorApp.Library;
    using DemoBlazorApp.Models;

    using Microsoft.AspNetCore.Components;

    /// <summary>
    ///     The table.
    /// </summary>
    public partial class TableComponent
    {
        /// <summary>
        /// The my table.
        /// </summary>
        private DynamicTable table = null;

        /// <summary>
        ///     The selected table.
        /// </summary>
        private TableType selectedTableType;

        /// <summary>
        ///     Gets or sets the selected table.
        /// </summary>
        [Parameter]
        public TableType SelectedTableType
        {
            get => this.selectedTableType;

            set
            {
                this.selectedTableType = value;
                this.DisplayTable();
            }
        }

        /// <summary>
        /// The get prop value.
        /// </summary>
        /// <param name="src">
        /// The source.
        /// </param>
        /// <param name="propName">
        /// The prop name.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        private static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName)?.GetValue(src, null);
        }

        /// <summary>
        /// The display table.
        /// </summary>
        private void DisplayTable()
        {
            if (this.selectedTableType is null)
            {
                return;
            }

            Console.WriteLine("Display Table: " + this.selectedTableType);

            switch (this.selectedTableType.Name)
            {
                case nameof(TableOneModel):

                    var modelOneList = new List<TableOneModel>
                                           {
                                               new TableOneModel { Id = 1, Name = "John", Email = "test1@email.com" },
                                               new TableOneModel { Id = 2, Name = "Doe", Email = "test2@email.com" }
                                           };

                    this.table = this.GetDynamicTable(modelOneList);

                    break;
                case nameof(TableTwoModel):

                    var modelTwoList = new List<TableTwoModel>
                                           {
                                               new TableTwoModel
                                                   {
                                                       Id = 1, Name = "Mohammed", Phone = "2140000000", Note = "Nothing"
                                                   },
                                               new TableTwoModel
                                                   {
                                                       Id = 1,
                                                       Name = "Test User",
                                                       Phone = "9720000000",
                                                       Note = "Some note"
                                                   }
                                           };

                    this.table = this.GetDynamicTable(modelTwoList);

                    break;
            }
        }

        /// <summary>
        /// The get my table.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        /// <typeparam name="T">
        /// The list item type.
        /// </typeparam>
        /// <returns>
        /// The <see cref="DynamicTable"/>.
        /// </returns>
        private DynamicTable GetDynamicTable<T>(List<T> items)
        {
            var myTable = new DynamicTable();

            var props = typeof(T).GetProperties().ToList();

            for (var i = 0; i < props.Count; i++)
            {
                myTable.Columns.Add(new TableColumn { Index = i, Name = props[i].Name });
            }

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var row = item.ToTableRow(i);
                myTable.Rows.Add(row);
            }

            return myTable;
        }
        
        /// <summary>
        /// The on row change.
        /// </summary>
        /// <param name="row">
        /// The row.
        /// </param>
        private void OnRowChange(TableRow row)
        {
            var obj = this.table.Rows.FirstOrDefault(r => r.Index == row.Index);

            if (obj != null && this.selectedTableType != null)
            {
                if (this.selectedTableType.Name == nameof(TableOneModel))
                {
                    var updatedObject = this.ConvertTableRowToType<TableOneModel>(row);
                    var index = this.table.Rows.IndexOf(row);

                    if (index != -1)
                    {
                        Console.WriteLine($"Name: {updatedObject.Name}");
                        this.table.Rows[index] = updatedObject.ToTableRow(index);
                    }
                }
            }
        }

        /// <summary>
        /// The convert table row to type.
        /// </summary>
        /// <param name="row">
        /// The row.
        /// </param>
        /// <typeparam name="T">
        /// The type.
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        private T ConvertTableRowToType<T>(TableRow row)
        {
            var type = typeof(T);
            var obj = Activator.CreateInstance(type);
            var properties = type.GetProperties();

            foreach (var propertyInfo in properties)
            {
                var cell = row.Cells.FirstOrDefault(c => c.ColumnName == propertyInfo.Name);
                
                if(cell != null)
                {
                    var newValue = Convert.ChangeType(cell.Value, propertyInfo.PropertyType);
                    propertyInfo.SetValue(obj, newValue, null);
                }
            }

            return (T)obj;
        }
    }
}