namespace SOM.ProductService.Domain.Product;

public class ProductGroup
{
    public ProductGroup()
    {
        ProductProductGroups = new HashSet<ProductProductGroupMapping>();
    }
    public int Id { get; set; }
    public string GroupName { get; set; } = null!;
    
    public virtual ICollection<ProductProductGroupMapping> ProductProductGroups { get; set; }
}