using Microsoft.EntityFrameworkCore;
using Projekt.Data;

namespace Projekt.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {

                // Look for any movies.
                if (context.Producer.Any())
                {
                    return;   // DB has been seeded
                }
         
                context.Producer.AddRange(
                    new Producer
                    {
                        Name = "Universal",
                        Country = "US",
                        YearOfFoundation = 1912
                    },
                    new Producer
                    {
                        Name = "Warner Bros.",
                        Country = "US",
                        YearOfFoundation = 1923
                    },
                    new Producer
                    {
                        Name = "Młode Studio",
                        Country = "Polska",
                        YearOfFoundation = 2005
                    },
                    new Producer
                    {
                        Name = "YoungProducers",
                        Country = "UK",
                        YearOfFoundation = 2004
                    }
                );
                context.SaveChanges();
            }
        }
    }


}
