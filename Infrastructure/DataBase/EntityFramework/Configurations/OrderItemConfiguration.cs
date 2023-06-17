using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataBase.EntityFramework.Configurations
{
    internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(p => p.Id)
                 .ValueGeneratedOnAdd()
                 .HasColumnType("uniqueidentifier");

            builder.ToTable("OrderItems");
        }
    }
}
