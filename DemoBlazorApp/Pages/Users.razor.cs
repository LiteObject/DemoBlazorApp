using DemoBlazorApp.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using DemoBlazorApp.Library;

namespace DemoBlazorApp.Pages
{
    public partial class Users
    {
        private UserApiResponse userApiResponse;

        [Inject]
        public HttpClient Client { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            await FetchUsers();
            await base.OnInitializedAsync();
        }

        private async Task FetchUsers()
        {
            try
            {
                await Task.Delay(2000);
                this.Client.BaseAddress = new Uri("https://randomuser.me");
                var jsonStream = await this.Client.GetStreamAsync("/api?results=10");
                userApiResponse = await JsonSerializer.DeserializeAsync<UserApiResponse>(jsonStream);
            }
            catch (Exception a)
            {
                Util.Log(a);
                // throw;
            }
        }

        public void OnUpdate(Result user) {
            Console.WriteLine($"User: {user.Email}");
        }
    }
}
