using Core.Domain;
using Infrastructure.DataBase.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Web.Security
{
    public static class SecurityConfigurationExtensions
    {

        public static void ConfigureIdentity(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IUserClaimsPrincipalFactory<User>, UserClaimsPrincipalFactory>();

            services.AddDbContext<ApplicationDbContext>(op => op.UseSqlServer(configuration.GetConnectionString("Default")));

            services.AddIdentity<User, Role>()
                  .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }
            public static void ConfigurePermission(this IServiceCollection services)
            {
                services.AddAuthorization(options =>
                {
                    foreach (var type in typeof(Permissions).GetNestedTypes())
                    {
                        foreach (var field in type.GetFields(BindingFlags.Static | BindingFlags.Public).Where(y => y.IsLiteral))
                        {

                            options.AddPolicy(field.GetValue(null).ToString(),
                                policy => policy.RequireClaim(field.GetValue(null).ToString()));
                        }
                    }

                    options.AddPolicy(DoctorRole.Doctor, p => p.RequireClaim(DoctorRole.Doctor));
                    //options.AddPolicy(ActiveUserRole.ActiveUser, p => p.RequireClaim(ActiveUserRole.ActiveUser));
                });
            }
        
    }
}
