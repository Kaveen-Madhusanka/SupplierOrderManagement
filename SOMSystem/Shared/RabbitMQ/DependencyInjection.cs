using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using SOM.Shared.Interfaces;

namespace RabbitMQ;

public static class DependencyInjection
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMessagePublisher, MessagePublisher>();
        
        services.AddSingleton<IConnectionFactory>(serviceProvider => new ConnectionFactory
        {
            Uri = new Uri(configuration.GetConnectionString("EventBus"))
        });

        return services;
    }
}