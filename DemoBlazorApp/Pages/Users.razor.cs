using DemoBlazorApp.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoBlazorApp.Pages
{
    public partial class Users
    {
        List<User> users = new List<User> {
        new User() { Id = 1, Name = "Mohammed", Email = "mhoque@email.com"},
        new User() { Id = 2, Name = "John", Email = "john@email.com"},
        };

        public void OnUpdate(User user) {
            Console.WriteLine($"User: {user.Name}");
        }
    }
}
