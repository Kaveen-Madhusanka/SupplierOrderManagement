namespace SOM.ProductService.Domain.Product;

public class ProductProductGroupMapping
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int ProductGroupId { get; set; }
    
    public virtual ProductGroup StockGroup { get; set; } = null!;
    public virtual Product StockItem { get; set; } = null!;
}