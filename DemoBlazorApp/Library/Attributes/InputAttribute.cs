namespace DemoBlazorApp.Library
{
    using System;

    /// <summary>
    /// The input attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class HtmlInputAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlInputAttribute"/> class.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <example>
        /// InputAttribute(key="type", value="number")
        /// InputAttribute(key="min", value="1")
        /// InputAttribute(key="max", value="100")
        /// </example>
        public HtmlInputAttribute(string key, string value)
        {
            this.Key = key;
            this.Value = value;

            /*try
            {
                this.attr.Add(key, value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            } */
        }

        /*/// <summary>
        /// Initializes a new instance of the <see cref="InputAttribute"/> class.
        /// </summary>
        /// <param name="arr">
        /// The arguments.
        /// </param>
        /// <example>
        /// InputAttribute("type:number", "min:1", "max:100")]
        /// </example>
        public InputAttribute(params string[] arr)
        {
            try
            {
                this.attr = arr.ToDictionary(x => x.Split(':')[0], x => x.Split(':')[1]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        } */

        /// <summary>
        /// Gets the key.
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Gets value.
        /// </summary>
        public string Value { get; }
    }
}