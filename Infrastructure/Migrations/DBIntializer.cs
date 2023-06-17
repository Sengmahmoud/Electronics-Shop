using Core.Domain;
using Infrastructure.DataBase.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Migrations
{
    public static class DBIntializer
    {
    

        public static async Task InitIdentity(IServiceProvider serviceProvider)
        {

            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            var roleName = "Administrator";
            var roleExist = await roleManager.RoleExistsAsync(roleName);

            if (!roleExist)
            {
                var result = await roleManager
                    .CreateAsync(new Role { Name = roleName });

                if (result.Succeeded)
                {
                    var userManager = serviceProvider
                        .GetRequiredService<UserManager<User>>();
               //     var config = serviceProvider
                  //      .GetRequiredService<IConfiguration>();
                    var admin = await userManager
                        .FindByEmailAsync("admin@test.com");

                    if (admin == null)
                    {
                        admin = new User
                        {
                            UserName = "admin@test.com",
                            Email = "admin@test.com",
                            EmailConfirmed = true,
                            Mobile="011111111",
                            Name="admin",
                             Active=true
                        };
                        result = await userManager
                            .CreateAsync(admin, "Admin1$");

                        if (result.Succeeded)
                        {
                            result = await userManager
                                .AddToRoleAsync(admin, roleName);
                            if (!result.Succeeded)
                            {
                                // todo: process errors
                            }
                        }
                    }
                }
            }
        }
    } 
}

