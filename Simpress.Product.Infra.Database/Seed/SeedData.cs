using Microsoft.EntityFrameworkCore;
using Simpress.Product.Domain.Entities.Models;

namespace Simpress.Product.Infra.Database.Seed
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData
                (
                    new Category(1,"Sports"),
                    new Category(2,"Technology")

                );

            modelBuilder.Entity<Domain.Entities.Models.Product>().HasData
                (

                    new Domain.Entities.Models.Product(1,"Ball", 29.99m, 1),
                    new Domain.Entities.Models.Product(2,"NoteBook", 5000m, 2)


                );
        }
    }
}
