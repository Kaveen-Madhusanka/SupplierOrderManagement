using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using SOM.Shared.SettingOptions;
using IConnectionFactory = RabbitMQ.Client.IConnectionFactory;

namespace RabbitMQ;

// TODO: need to refact

public abstract class ConsumerBase(IConnectionFactory connectionFactory, 
        IServiceScopeFactory serviceScopeFactory,
        IOptions<EventBusOptions> eventBusOptions, 
        List<string> bindingKeys)
    : IHostedService, IDisposable
{
    protected IModel Channel;
    protected readonly IServiceScopeFactory ServiceScopeFactory = serviceScopeFactory;
    protected readonly IOptions<EventBusOptions> EventBusOptions = eventBusOptions;

    private readonly int _maxRetries = 20; // Maximum number of retries
    private readonly TimeSpan _retryDelay = TimeSpan.FromSeconds(5);

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Channel = Connect();
        Channel.ExchangeDeclare(EventBusOptions.Value.ExchangeName, ExchangeType.Topic);
        Channel.QueueDeclare(EventBusOptions.Value.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        foreach (var bindingKey in bindingKeys)
        {
            Channel.QueueBind(EventBusOptions.Value.QueueName, EventBusOptions.Value.ExchangeName, bindingKey);
        }

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

    public IModel Connect()
    {
        var retryPolicy = Policy
            .Handle<BrokerUnreachableException>()
            .WaitAndRetry(_maxRetries, retryAttempt => _retryDelay,
                (exception, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine($"Connection attempt {retryCount} failed. Waiting {timeSpan} before next retry. Error: {exception.Message}");
                });

        IModel channel = null;
        retryPolicy.Execute(() =>
        {
            var connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
        });

        return channel;
    }

}