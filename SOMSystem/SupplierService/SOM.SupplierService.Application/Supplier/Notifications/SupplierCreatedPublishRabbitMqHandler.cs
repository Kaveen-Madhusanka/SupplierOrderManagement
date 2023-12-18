using MediatR;
using SOM.Shared.Interfaces;

namespace SOM.SupplierService.Application.Supplier.Notifications;

public class SupplierCreatedPublishRabbitMqHandler: INotificationHandler<SupplierCreated>
{
    private readonly IMessagePublisher _messagePublisher;
    
    public SupplierCreatedPublishRabbitMqHandler(IMessagePublisher messagePublisher)
    {
        _messagePublisher = messagePublisher;
    }
    
    public Task Handle(SupplierCreated supplierCreated, CancellationToken cancellationToken)
    {
        var publisherName = "supplier-service";
        var exchangeName = "som-exchanger";
        var queueName = "som-queue";
        var routingKey = "supplier-route-key";

        _messagePublisher.Publish(publisherName, exchangeName, queueName, routingKey, supplierCreated);
        return Task.CompletedTask;
    }
}