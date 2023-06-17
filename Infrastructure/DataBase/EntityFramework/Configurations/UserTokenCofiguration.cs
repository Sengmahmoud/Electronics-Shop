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
    public class UserTokenCofiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.Property<Guid>("UserId")
              .HasColumnType("uniqueidentifier");

            builder.Property<string>("LoginProvider")
                .HasColumnType("nvarchar(450)");

            builder.Property<string>("Name")
                .HasColumnType("nvarchar(450)");

            builder.Property<string>("Value")
                .HasColumnType("nvarchar(max)");

            builder.HasKey("UserId", "LoginProvider", "Name");

            builder.ToTable("UserTokens");
        }
    }
}
