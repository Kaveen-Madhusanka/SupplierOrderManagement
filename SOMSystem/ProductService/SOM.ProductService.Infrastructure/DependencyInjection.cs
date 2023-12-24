using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SOM.ProductService.Application.Common.Interface;
using SOM.ProductService.Infrastructure.Persistant;

namespace SOM.ProductService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddScoped<IProductDbContext, ProductDbContext>();
            
            services.AddDbContext<ProductDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                options.LogTo(Console.WriteLine);
            });

           

            return services;
        }
    }
}
