namespace DemoBlazorApp
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using Blazored.LocalStorage;
    using DemoBlazorApp.Library;
    using DemoBlazorApp.Services;

    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            // builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://randomuser.me") });

            builder.Services.AddScoped<ITableFactory, TableFactory>();
            builder.Services.AddSingleton<StateContainer>();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<ITableService, TableService>();
            builder.Services.AddSingleton<IMathService, MathService>();
            builder.Services.AddSingleton<IDynamicTableService, DynamicTableService>();

            await builder.Build().RunAsync();
        }
    }
}
