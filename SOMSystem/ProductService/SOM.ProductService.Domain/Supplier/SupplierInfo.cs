using System.ComponentModel.DataAnnotations.Schema;
using SOM.Shared.Entities;

namespace SOM.ProductService.Domain.Supplier;

public class SupplierInfo: AuditableEntity
{
    public SupplierInfo()
    {
        Products = new HashSet<Product.Product>();
    }
    
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public string SupplierName { get; set; }

    public virtual ICollection<Product.Product> Products { get; set; }
}