using Microsoft.EntityFrameworkCore;
using Simpress.Product.Domain.Entities.Models;
using Simpress.Product.Infra.Database.Seed;

namespace Simpress.Product.Infra.Database.Context
{
    public class SimpressContext : DbContext
    {
        public SimpressContext(DbContextOptions<SimpressContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Domain.Entities.Models.Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();

        }

    }
}
