using GuestService.Models;
using Microsoft.EntityFrameworkCore;

namespace GuestService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<GuestDbContext>(), isProd);
            }
        }

        private static void SeedData(GuestDbContext context, bool isProd)
        {
            if (isProd)
            {
                Console.WriteLine("Attempting to apply migrations..");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            if(!context.Guests.Any())
            {
                Console.WriteLine("Seeding data");
                context.Guests.AddRange(
                    new Guest() { Name = "Amra Bulic", CheckInDate = "31-12-2024", Email = "amra.bulic@gmail.com", CheckOutDate = "02-01-2025", PhoneNumber = "0628448077" },
                    new Guest() { Name = "Belma Hot", CheckInDate = "31-12-2024", Email = "belmahot@gmail.com", CheckOutDate = "02-01-2025", PhoneNumber = "0638448077" },
                    new Guest() { Name = "Anida Fakic", CheckInDate = "31-12-2024", Email = "anida.fakic@gmail.com", CheckOutDate = "02-01-2025", PhoneNumber = "0648448077" }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("We already have data");
            }
        }
    }
}
