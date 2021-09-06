using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Models.Identity;
using DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GuessTheNumber
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var webHost = CreateHostBuilder(args).Build();
            using var scope = webHost.Services.CreateScope();
            var env = scope.ServiceProvider.GetService<IWebHostEnvironment>();
            if (env.IsDevelopment())
            {
                Console.Title = "dotnet.exe - Interact";
            }

            await DatabaseInitializer.Initialize(scope);
            await webHost.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}