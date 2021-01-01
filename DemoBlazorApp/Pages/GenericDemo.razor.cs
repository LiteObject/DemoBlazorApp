using DemoBlazorApp.Library;

namespace DemoBlazorApp.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DemoBlazorApp.Models;
    using DemoBlazorApp.Models.Pet;

    using Microsoft.AspNetCore.Components;

    /// <summary>
    /// The generic demo.
    /// </summary>
    public partial class GenericDemo
    {
        private string selectedModel = string.Empty;

        private Type selectedModelType;

        List<GenericBase<IEntity>> items;

        List<string> columns = new List<string>();

        /// <summary>
        /// The on selection.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        public void OnSelection(ChangeEventArgs e)
        {
            this.selectedModel = e.Value?.ToString() ?? string.Empty;
            Util.Log($"Selected Model: {this.selectedModel}");

            switch (this.selectedModel)
            {
                case "pet":
                case nameof(Pet):
                    this.selectedModelType = typeof(Pet);
                    this.columns.Clear();
                    this.SetColumns(typeof(Pet));
                    this.items = new List<GenericBase<IEntity>>(new List<Pet>
                                                                    {
                                                                        new Pet { Id = 1, Name = "Mr.Fluffy", OwnerName = "Mohammed"},
                                                                        new Pet { Id = 2, Name = "Mr.Coffee", OwnerName = "John Doe"},
                                                                    });
                    break;
                case "person":
                case "Person":
                    this.selectedModelType = typeof(Person);
                    this.columns.Clear();
                    this.SetColumns(typeof(Person));
                    this.items = new List<GenericBase<IEntity>>(new List<Person>
                                                                    {
                                                                        new Person
                                                                            {
                                                                                Id = 1,
                                                                                Name = "Mohammed",
                                                                                Email = "mohammed@email.com"
                                                                            },
                                                                        new Person
                                                                            {
                                                                                Id = 2,
                                                                                Name = "John Doe",
                                                                                Email = "john@email.com"
                                                                            },
                                                                    });
                    break;
                default:
                    break;
            }
        }

        private void SetColumns(Type type)
        {
            var cols = type.GetProperties().Select(p => p.Name).ToList();
            cols.ForEach(c => Util.Log(c));
            this.columns.AddRange(cols);
        }
    }
}
