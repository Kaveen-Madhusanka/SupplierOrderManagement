namespace SOM.ProductService.Domain.Color;

public class Color
{

    public Color()
    {
        Products = new HashSet<Product.Product>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public virtual ICollection<Product.Product> Products { get; set; }
}