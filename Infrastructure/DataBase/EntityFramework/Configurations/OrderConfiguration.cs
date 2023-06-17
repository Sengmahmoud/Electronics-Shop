using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataBase.EntityFramework.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(p => p.Id)
                 .ValueGeneratedOnAdd()
                 .HasColumnType("uniqueidentifier");

            builder.ToTable("Orders");
        }
    }
}
