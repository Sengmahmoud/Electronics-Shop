using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataBase.EntityFramework.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(p => p.Id)
                 .ValueGeneratedOnAdd()
                 .HasColumnType("uniqueidentifier");

            builder.Property(p => p.Discount)
                .HasDefaultValue(0);
            builder.ToTable("Products");
        }
    }
}
