using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Simpress.Product.Infra.Database.Mapping
{
    public class ProductMapping : IEntityTypeConfiguration<Domain.Entities.Models.Product>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Models.Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Description).IsRequired().HasMaxLength(50);

            builder.Property(x => x.Price).IsRequired().HasPrecision(5,2);

            
        }
    }
}
