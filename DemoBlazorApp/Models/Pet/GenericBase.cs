namespace DemoBlazorApp.Models.Pet
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The generic base.
    /// Variant param "out" can only be declared in interfaces and delegates.
    /// </summary>
    /// <typeparam name="T">
    /// The type.
    /// </typeparam>
    public class GenericBase<T> : IGenericBase<T>
        where T : IEntity
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /*// <summary>
        /// The get properties.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        public IEnumerable<string> GetProperties()
        {
            // This will not return derive type properties
            return typeof(T).GetProperties().Select(p => p.Name);
        } */

        public string GetInfo()
        {
            // DemoBlazorApp.Models.Pet.IEntity has a Name with value Mohammed
            return $"{typeof(T)} has a {nameof(Name)} with value {this.Name}";
        }
    }
}
