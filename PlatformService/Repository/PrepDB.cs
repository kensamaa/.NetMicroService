using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.Data
{
    public static class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
        }
        private static void SeedData(AppDbContext context)
        {
            if (!context.platforms.Any())
            {
                Console.WriteLine("-->Seeding data");
                context.platforms.AddRange(
                    new Models.Platform() { Name="dotnet",Publisher="Microsoft",Cost="Free"},
                    new Models.Platform() { Name = "sql server", Publisher = "Microsoft", Cost = "Free" },
                    new Models.Platform() { Name = "kubernetes", Publisher = "CNCF", Cost = "Free" }
                    );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("-->we already have data");
            }

        }

    }
}
