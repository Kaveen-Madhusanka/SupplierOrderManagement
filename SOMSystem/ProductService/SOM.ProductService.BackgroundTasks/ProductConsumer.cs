using System.Text;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Enums;
using SOM.ProductService.Application.SupplierInfo.Commands;
using SOM.ProductService.Domain.Supplier;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BackgroundTasks;

public class ProductConsumer : IHostedService, IDisposable
{
    protected string QueueName => "productServiceQueue_dev_v1";

    protected List<string> BindingKeys = new()
    {
        SupplierEventEnum.SupplierCreated.ToString(),
        SupplierEventEnum.SupplierUpdated.ToString(),
        SupplierEventEnum.SupplierDeleted.ToString()
    };

    private IModel _channel;

    private const string ExchangeName = "som-exchange";

    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ProductConsumer(IConnectionFactory connectionFactory, IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
        var connection = connectionFactory.CreateConnection();
        _channel = connection.CreateModel();

        _channel.ExchangeDeclare(ExchangeName, ExchangeType.Topic);
        _channel.QueueDeclare(QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        foreach (var bindingKey in BindingKeys)
        {
            _channel.QueueBind(QueueName, ExchangeName, bindingKey);
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
            Console.WriteLine($"Received message in {QueueName} with routing key {routingKey}: {message}");

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

        _channel.BasicConsume(queue: QueueName, autoAck: true, consumer: consumer);

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