namespace DemoBlazorApp.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;
    using DemoBlazorApp.Library;

    /// <summary>
    /// The base mode.
    /// </summary>
    public abstract class BaseModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            Console.WriteLine($"{nameof(OnPropertyChanged)} has been invoked by {name}");            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Key]
        [Order(1)]
        [HtmlInput(key: "disabled", value: "disabled")]
        [HtmlInput(key: "type", value: "number")]
        public int Id { get; set; } = 0;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Order(2)]
        public string Name { get; set; } = "Not available";

        public virtual void UpdateModel(Action action)
        {
            if (action != null)
            {
                action.Invoke();
            }            
        }
    }
}
