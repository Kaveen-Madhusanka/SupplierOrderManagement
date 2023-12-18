using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SOM.Shared.Interfaces;

namespace RabbitMQ;

public static class DependencyInjection
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services)
    {
        services.AddScoped<IMessagePublisher, MessagePublisher>();
        return services;
    }
}