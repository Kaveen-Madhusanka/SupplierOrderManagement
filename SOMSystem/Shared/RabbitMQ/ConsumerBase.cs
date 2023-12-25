using System.Text;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SOM.Shared.SettingOptions;

namespace RabbitMQ;

public abstract class ConsumerBase : IHostedService, IDisposable
{
    protected IModel Channel;
    protected readonly IServiceScopeFactory ServiceScopeFactory;
    protected readonly IOptions<EventBusOptions> EventBusOptions;

    protected ConsumerBase(IConnectionFactory connectionFactory, IServiceScopeFactory serviceScopeFactory, IOptions<EventBusOptions> eventBusOptions, List<string> BindKeys)
    {
        ServiceScopeFactory = serviceScopeFactory;
        EventBusOptions = eventBusOptions;

        var connection = connectionFactory.CreateConnection();
        Channel = connection.CreateModel();
        Channel.ExchangeDeclare(EventBusOptions.Value.ExchangeName, ExchangeType.Topic);
        Channel.QueueDeclare(EventBusOptions.Value.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        
        foreach (var bindingKey in BindKeys)
        {
            Channel.QueueBind(EventBusOptions.Value.QueueName, EventBusOptions.Value.ExchangeName, bindingKey);
        }
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var consumer = new EventingBasicConsumer(Channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var routingKey = ea.RoutingKey;
            Console.WriteLine($"Received message in {EventBusOptions.Value.QueueName} with routing key {routingKey}: {message}");

            await ProcessMessageAsync(message, routingKey, cancellationToken);
        };

        Channel.BasicConsume(queue: EventBusOptions.Value.QueueName, autoAck: true, consumer: consumer);

        return Task.CompletedTask;
    }

    protected abstract Task ProcessMessageAsync(string message, string routingKey, CancellationToken cancellationToken);

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Channel.Close();
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        Channel?.Dispose();
    }
}