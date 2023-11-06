using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SOM.ProductService.Application.Common.Interface;
using SOM.ProductService.Infastructure.Persistant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOM.ProductService.Infastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DevConnection"));
                options.LogTo(Console.WriteLine);
            });

            services.AddScoped<IAppDbContext, AppDbContext>();

            return services;
        }
    }
}
