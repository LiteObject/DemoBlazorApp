namespace DemoBlazorApp.Library
{
    using System;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// The order attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class OrderAttribute : Attribute
    {
        /// <summary>
        /// The order.
        /// </summary>
        private readonly int order;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderAttribute"/> class.
        /// </summary>
        /// <param name="order">
        /// The order.
        /// </param>
        public OrderAttribute([CallerLineNumber] int order = 0)
        {
            this.order = order;
        }

        /// <summary>
        /// Gets the order.
        /// </summary>
        public int Order => this.order;
    }
}
