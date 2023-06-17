using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataBase.EntityFramework.Configurations
{
    public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.Property<string>("LoginProvider")
                .HasColumnType("nvarchar(450)");

            builder.Property<string>("ProviderKey")
                .HasColumnType("nvarchar(450)");

            builder.Property<string>("ProviderDisplayName")
                .HasColumnType("nvarchar(max)");

            builder.Property<Guid>("UserId")
                .HasColumnType("uniqueidentifier");

            builder.HasKey("LoginProvider", "ProviderKey");

            builder.HasIndex("UserId");

            builder.ToTable("UserLogins");
        }
    }
}
