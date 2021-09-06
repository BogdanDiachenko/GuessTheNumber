using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    public static class DatabaseInitializer
    {
        public static async Task Initialize(IServiceScope scope)
        {
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            var manager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
            if (context != null && (await context.Database.GetPendingMigrationsAsync()).Any())
            {
                await context.Database.MigrateAsync();
                await SeedData(manager);
            }
        }

        private static async Task SeedData(UserManager<ApplicationUser> manager)
        {
            if (manager.Users.Any())
            {
                return;
            }

            var users = new List<ApplicationUser>()
            {
                new()
                {
                    Name = "Anton", Surname = "Antonenko", UserName = "Antoshka-Kartoshka",
                    Email = "antoha@gmail.com"
                },
                new() {Name = "Vasiliy", Surname = "Vasechkin", UserName = "Vasilek", Email = "vasya@gmail.com"},
                new() {Name = "Ivan", Surname = "Ivanov", UserName = "VanGogh", Email = "vanya@gmail.com"}
            };

            foreach (var user in users)
            {
                await manager.CreateAsync(user, "14881488z");
            }
        }
    }
}