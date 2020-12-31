namespace DemoBlazorApp.Models.Pet
{
    using System.Collections.Generic;

    /// <summary>
    /// The GenericBase interface.
    /// Variant param "out" can only be declared in interfaces and delegates.
    /// </summary>
    /// <typeparam name="T">
    /// The type.
    /// </typeparam>
    public interface IGenericBase<out T>
        where T : IEntity
    {
        /// <summary>
        /// The get properties.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        // public IEnumerable<string> GetProperties();

        /// <summary>
        /// The get name.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetInfo();
    }
}