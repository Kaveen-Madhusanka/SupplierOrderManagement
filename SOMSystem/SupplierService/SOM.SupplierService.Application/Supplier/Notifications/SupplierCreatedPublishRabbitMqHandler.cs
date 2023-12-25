using MediatR;
using SOM.Shared.Enums;
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
        _messagePublisher.Publish(SupplierEventEnum.SupplierCreated, supplierCreated);
        return Task.CompletedTask;
    }
}