using MediatR;

namespace SOM.SupplierService.Application.Supplier.Notifications;

public class SupplierCreatedSendSmsHandler: INotificationHandler<SupplierCreated>
{
    public Task Handle(SupplierCreated notification, CancellationToken cancellationToken)
    {
        Console.WriteLine("send sms notification");
        return Task.CompletedTask;
    }
}