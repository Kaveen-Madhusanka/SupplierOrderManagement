using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SOM.SupplierService.Application.Common.Interface;
using SOM.SupplierService.Infrastructure.Persistant;

namespace SOM.SupplierService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SupplierDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                options.LogTo(Console.WriteLine);
            });

            services.AddScoped<ISupplierDbContext, SupplierDbContext>();

            return services;
        }
    }
}
