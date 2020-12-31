namespace DemoBlazorApp.Services
{
    using System;
    using System.Linq;

    public class MathService : IMathService
    {
        public double Add(params double[] numbers)
        {
            return numbers.Sum();
        }
    }
}
