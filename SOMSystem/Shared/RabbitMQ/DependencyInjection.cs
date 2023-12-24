using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using SOM.Shared.Interfaces;

namespace RabbitMQ;

public static class DependencyInjection
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services)
    {
        services.AddScoped<IMessagePublisher, MessagePublisher>();
        
        
        services
            // other registrations omitted for brevity
            .AddSingleton<IConnectionFactory>(serviceProvider =>
            {
                var uri = new Uri("amqp://guest:guest@localhost:5672");
                return new ConnectionFactory
                {
                    Uri = uri
                };
            });
        
        
        return services;
    }
}