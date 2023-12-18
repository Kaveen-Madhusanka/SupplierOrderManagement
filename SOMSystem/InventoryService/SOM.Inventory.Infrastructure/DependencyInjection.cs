using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SOM.Inventory.Application.Common.Interface;
using SOM.Inventory.Infrastructure.Persistant;

namespace SOM.Inventory.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<InventoryDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            options.LogTo(Console.WriteLine);
        });

        services.AddScoped<IInventoryDbContext, InventoryDbContext>();

        return services;
    }
}