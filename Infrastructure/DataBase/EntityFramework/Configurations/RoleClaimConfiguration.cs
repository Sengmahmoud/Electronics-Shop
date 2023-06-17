using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataBase.EntityFramework.Configurations
{
    public class RoleClaimConfiguration: IEntityTypeConfiguration<RoleClaim>
    {
       
public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int")
                .HasAnnotation("SqlServer:ValueGenerationStrategy",
                    SqlServerValueGenerationStrategy.IdentityColumn);

            builder.Property<string>("ClaimType")
                    .HasColumnType("nvarchar(max)");

            builder.Property<string>("ClaimValue")
                    .HasColumnType("nvarchar(max)");

            builder.Property<Guid>("RoleId")
                    .HasColumnType("uniqueidentifier");

            builder.HasKey("Id");

            builder.HasIndex("RoleId");

            //builder.HasOne("Core.Domain.Role", null)
            //    .WithMany()
            //    .HasForeignKey("RoleId")
            //    .OnDelete(DeleteBehavior.Cascade)
            //    .IsRequired();

            builder.ToTable("RoleClaims");
        }
    }
}
