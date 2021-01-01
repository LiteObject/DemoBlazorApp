namespace DemoBlazorApp.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;
    using Services;
    using Library;

    /// <summary>
    /// The base mode.
    /// </summary>
    public abstract class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            Util.Log($"{nameof(OnPropertyChanged)} has been invoked by {name}");            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            GetTotal();
        }

        /* protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, e);
        } */

        protected readonly IMathService MathService;

        protected BaseModel(IMathService service)
        {
            this.MathService = service ?? throw new ArgumentNullException(nameof(service));
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

        public abstract void GetTotal();

        public virtual void UpdateModel(Action action)
        {
            action?.Invoke();
        }
    }
}
