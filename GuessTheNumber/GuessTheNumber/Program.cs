using System;
using System.Linq;
using DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GuessTheNumber
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateHostBuilder(args).Build();
            using (IServiceScope scope = webHost.Services.CreateScope())
            {
                var env = scope.ServiceProvider.GetService<IWebHostEnvironment>();
                if (env.IsDevelopment())
                {
                    Console.Title = "dotnet.exe - Interact";
                }

                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                webHost.Run();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}