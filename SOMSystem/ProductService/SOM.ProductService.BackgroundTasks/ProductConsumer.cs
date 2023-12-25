using System.Text;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SOM.ProductService.Application.SupplierInfo.Commands;
using SOM.ProductService.Domain.Supplier;
using SOM.Shared.Enums;
using SOM.Shared.SettingOptions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SOM.ProductService.BackgroundTasks;

public class ProductConsumer : IHostedService, IDisposable
{
    protected List<string> BindingKeys = new()
    {
        SupplierEventEnum.SupplierCreated.ToString(),
        SupplierEventEnum.SupplierUpdated.ToString(),
        SupplierEventEnum.SupplierDeleted.ToString()
    };

    private IModel _channel;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IOptions<EventBusOptions> _eventBusOptions;

    public ProductConsumer(IConnectionFactory connectionFactory, IServiceScopeFactory serviceScopeFactory, IOptions<EventBusOptions> eventBusOptions)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _eventBusOptions = eventBusOptions;
        var connection = connectionFactory.CreateConnection();
        _channel = connection.CreateModel();

        _channel.ExchangeDeclare(_eventBusOptions.Value.ExchangeName, ExchangeType.Topic);
        _channel.QueueDeclare(_eventBusOptions.Value.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        foreach (var bindingKey in BindingKeys)
        {
            _channel.QueueBind(_eventBusOptions.Value.QueueName, _eventBusOptions.Value.ExchangeName, bindingKey);
        }
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var routingKey = ea.RoutingKey;
            Console.WriteLine($"Received message in {_eventBusOptions.Value.QueueName} with routing key {routingKey}: {message}");

            using var scope = _serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            if (routingKey == SupplierEventEnum.SupplierCreated.ToString())
            {
                var supplierInfo = JsonSerializer.Deserialize<SupplierInfo>(message);
                await mediator.Send(new CreateSupplierInfoCommand()
                {
                    Id = supplierInfo!.Id,
                    Name = supplierInfo.SupplierName
                }, cancellationToken);
            }
        };

        _channel.BasicConsume(queue: _eventBusOptions.Value.QueueName, autoAck: true, consumer: consumer);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _channel.Close();
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _channel?.Dispose();
    }
}