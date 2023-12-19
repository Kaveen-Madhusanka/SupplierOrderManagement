using MediatR;

namespace SOM.SupplierService.Application.Supplier.Notifications;

public class SupplierCreated : INotification
{
    public int Id { get; set; }
    public string SupplierName { get; set; }

    public SupplierCreated(int id, string supplierName)
    {
        Id = id;
        SupplierName = supplierName;
    }
}