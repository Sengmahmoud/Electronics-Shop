using AutoMapper;
using Core.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AutoMapper
{
    public static class AutoMapperServicesConfigurationExtensions
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            //services.AddAutoMapper(typeof(MappingProfile));


            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
