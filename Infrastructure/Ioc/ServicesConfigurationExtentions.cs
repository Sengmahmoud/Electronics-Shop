using Core.Persistence;
using Core.Services.Implementation;
using Core.Services.Interfaces;
using Infrastructure.DataBase.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure.Ioc;

public static class ServicesConfigurationExtentions
{
    public static void Configureioc(this IServiceCollection service)
    {
        service.AddScoped<IApplicationDbContext, ApplicationDbContext>();
      

        service.AddScoped<ISecurityService, SecurityService>();
        service.AddScoped<ILockUpService, LockUpService>();
        service.AddScoped<IProductService, ProductService>();
        service.AddScoped<ICategoryService, CategoryService>();
        service.AddScoped<IOredrService, OredrService>();

    }
}
