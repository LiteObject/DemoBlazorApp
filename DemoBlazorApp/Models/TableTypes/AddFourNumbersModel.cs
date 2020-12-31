namespace DemoBlazorApp.Models.TableTypes
{
    using Services;
    using Library;
    using System;
    using System.ComponentModel;

    public class AddFourNumbersModel : BaseModel
    {
        private int number1 = 0;
        private int number2 = 0;
        private int number3 = 0;
        private int number4 = 0;

        public AddFourNumbersModel(IMathService service) : base(service)
        {
            // ToDo: Assign handler from component & dispose (unsubscribe)
            // PropertyChanged += (sender, arg) => GetTotal();
        }

        /// <summary>
        /// Gets or sets the number 1.
        /// </summary>
        [Order]
        [Description("Num 1")]
        [HtmlInput(key: "type", value: "number")]
        [HtmlInput(key: "min", value: "0")]
        [HtmlInput(key: "step", value: "10")]
        [HtmlInput(key: "max", value: "100")]
        public int Number1
        {
            get => number1;
            set
            {
                number1 = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Gets or sets the number 2.
        /// </summary>
        [Order]
        [Description("Num 2")]
        [HtmlInput(key: "type", value: "number")]
        public int Number2
        {
            get => number2;
            set
            {
                number2 = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the number 3.
        /// </summary>
        [Order]
        [Description("Num 3")]
        [HtmlInput(key: "type", value: "number")]
        public int Number3
        {
            get => number3;
            set
            {
                number3 = value;
                OnPropertyChanged();
            }
        }

        [Order]
        [Description("Num 4")]
        [HtmlInput(key: "type", value: "number")]
        public int Number4
        {
            get => number4;
            set
            {
                number4 = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        [Order]
        [HtmlInput(key: "type", value: "number")]
        [HtmlInput(key: "readonly", value: "readonly")]
        public double Total { get; set; } = 0;

        /// <summary>
        /// The get total.
        /// </summary>
        public override void GetTotal()
        {
            this.Total = base.MathService.Add(this.Number1, this.Number2, this.Number3, this.Number4);
            Console.WriteLine($"{nameof(this.Total)} has been set to {this.Total}");
        }
    }
}
