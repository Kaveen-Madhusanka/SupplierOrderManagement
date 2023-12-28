using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using SOM.Shared.Interfaces;
using SOM.Shared.SettingOptions;

namespace RabbitMQ;

public static class DependencyInjection
{
    public static IServiceCollection AddRabbitMqEventBus(this IHostApplicationBuilder builder, string connectionStringName)
    {
        builder.Services.AddScoped<IMessagePublisher, MessagePublisher>();

        builder.Services.AddSingleton<IConnectionFactory>(serviceProvider => new ConnectionFactory
        {
            Uri = new Uri(builder.Configuration.GetConnectionString(connectionStringName)),
            AutomaticRecoveryEnabled = true,
        });

        builder.Services.Configure<EventBusOptions>(builder.Configuration.GetSection("EventBus"));

        return builder.Services;
    }
}