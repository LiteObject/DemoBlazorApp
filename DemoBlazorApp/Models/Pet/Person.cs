namespace DemoBlazorApp.Models.Pet
{
    /// <summary>
    /// The person.
    /// </summary>
    public class Person : GenericBase<IEntity>
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }
    }
}
