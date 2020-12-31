namespace DemoBlazorApp.Models
{
    using DemoBlazorApp.Library;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// The table math model.
    /// </summary>
    public class AddThreeNumbersModel : BaseModel
    {
        private int number1 = 0;
        private int number2 = 0;
        private int number3 = 0;

        public AddThreeNumbersModel()
        {
            PropertyChanged += (sender, r) => GetTotal();
        }

        /// <summary>
        /// Gets or sets the number 1.
        /// </summary>
        [Order]
        [HtmlInput(key: "type", value: "number")]
        [HtmlInput(key: "min", value: "0")]
        [HtmlInput(key: "step", value: "10")]
        [HtmlInput(key: "max", value: "100")]
        public int Number1 { 
            get => number1;
            set {
                number1 = value;
                OnPropertyChanged();
            } 
        }
        /// <summary>
        /// Gets or sets the number 2.
        /// </summary>
        [Order]
        [HtmlInput(key: "type", value: "number")]
        public int Number2 { 
            get => number2;
            set { 
                number2 = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the number 3.
        /// </summary>
        [Order]
        [HtmlInput(key: "type", value: "number")]
        public int Number3 { 
            get => number3;
            set { 
                number3 = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        [Order]
        [HtmlInput(key: "type", value: "number")]
        public int Total { get; set; } = 0;

        /// <summary>
        /// The get total.
        /// </summary>
        public void GetTotal()
        {
            this.Total = this.Number1 + this.Number2 + this.Number3;
            Console.WriteLine($"{nameof(this.Total)} has been set to {this.Total}");
        }
    }
}