using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataBase.EntityFramework.Configurations
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(p => p.Id)
                 .ValueGeneratedOnAdd()
                 .HasColumnType("uniqueidentifier");

            builder.ToTable("Customers");
        }
    }
}
