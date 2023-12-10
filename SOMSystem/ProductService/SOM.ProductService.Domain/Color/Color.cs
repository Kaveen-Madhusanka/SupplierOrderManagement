using SOM.Shared.Entities;

namespace SOM.ProductService.Domain.Color;

public class Color: AuditableEntity
{

    public Color()
    {
        Products = new HashSet<Product.Product>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public virtual ICollection<Product.Product> Products { get; set; }
}