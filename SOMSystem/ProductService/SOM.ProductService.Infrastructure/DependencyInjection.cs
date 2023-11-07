using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SOM.ProductService.Application.Common.Interface;
using SOM.ProductService.Infrastructure.Persistant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOM.ProductService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProductDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DevConnection"));
                options.LogTo(Console.WriteLine);
            });

            services.AddScoped<IProductDbContext, ProductDbContext>();

            return services;
        }
    }
}
