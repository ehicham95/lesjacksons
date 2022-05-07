using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProduction)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProduction);
            }
        }
        private static void SeedData(AppDbContext context, bool isProduction)
        {
            if(isProduction)
            {
                try
                {
                    context.Database.Migrate();
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"--> Could not run migration : {ex.Message}");
                }
            }
            if (!context.Platform.Any())
            {
                Console.WriteLine("We will start seeding data");
                context.Platform.Add(new Platform { Name = "Dot net", Publisher = "Microsoft", Cost = 0 });
                context.Platform.Add(new Platform { Name = "SQL Server", Publisher = "Microsoft", Cost = 0 });
                context.Platform.Add(new Platform { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = 0 });
                context.SaveChanges();
                Console.WriteLine("Seeding finished.");
            }
            else
            {
                Console.WriteLine("We already have data");
            }
        }
    }
}