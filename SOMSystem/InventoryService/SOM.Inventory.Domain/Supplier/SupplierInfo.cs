using SOM.Shared.Entities;

namespace SOM.Inventory.Domain.Supplier;

public class SupplierInfo : AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
}