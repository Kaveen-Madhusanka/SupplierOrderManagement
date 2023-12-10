using SOM.Inventory.Domain.Supplier;
using SOM.Shared.Entities;

namespace SOM.Inventory.Domain.Product;

public class ProductInfo: AuditableEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SupplierId { get; set; }

    public virtual SupplierInfo? Supplier { get; set; }
}