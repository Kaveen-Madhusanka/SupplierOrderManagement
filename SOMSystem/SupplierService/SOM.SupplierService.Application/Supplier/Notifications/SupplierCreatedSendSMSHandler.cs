using MediatR;

namespace SOM.SupplierService.Application.Supplier.Notifications;

public class SupplierCreatedSendSmsHandler: INotificationHandler<SupplierCreated>
{
    public Task Handle(SupplierCreated notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}