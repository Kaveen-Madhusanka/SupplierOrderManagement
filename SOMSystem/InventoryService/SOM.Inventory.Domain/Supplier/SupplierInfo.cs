using SOM.Inventory.Domain.Product;
using SOM.Shared.Entities;

namespace SOM.Inventory.Domain.Supplier;

public class SupplierInfo : AuditableEntity
{
    public SupplierInfo()
    {
        Products = new HashSet<ProductInfo>();
    }

    public int Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Product.ProductInfo> Products { get; set; }
}