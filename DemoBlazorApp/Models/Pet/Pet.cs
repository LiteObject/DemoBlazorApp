namespace DemoBlazorApp.Models.Pet
{
    /// <summary>
    /// The pet.
    /// </summary>
    public class Pet : GenericBase<IEntity>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string OwnerName { get; set; }
    }
}
