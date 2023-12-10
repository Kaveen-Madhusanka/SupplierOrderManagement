namespace SOM.ProductService.Domain.PackageType;

public class PackageType
{
    public PackageType()
    {
        OuterPackages = new HashSet<Product.Product>();
        //UnitPackages = new HashSet<Product.Product>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public virtual ICollection<Product.Product> OuterPackages { get; set; }
    //public virtual ICollection<Product.Product> UnitPackages { get; set; }
}