

using SOM.ProductService.Domain.Supplier;
using SOM.Shared.Entities;

namespace SOM.ProductService.Domain.Product
{
    public class Product : AuditableEntity
    {
        public Product()
        {
            ProductProductGroups = new HashSet<ProductProductGroupMapping>();
        }
        
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int SupplierId { get; set; }
        public int? ColorId { get; set; }
        public int UnitPackageId { get; set; }
        //public int OuterPackageId { get; set; }
        public string? Brand { get; set; }
        public string? Size { get; set; }
        public int LeadTimeDays { get; set; }
        public int QuantityPerOuter { get; set; }
        public bool IsChillerStock { get; set; }
        public string? Barcode { get; set; }
        public decimal TaxRate { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? RecommendedRetailPrice { get; set; }
        public decimal TypicalWeightPerUnit { get; set; }
        public string? MarketingComments { get; set; }
        public string? InternalComments { get; set; }
        public byte[]? Photo { get; set; }
        public string? CustomFields { get; set; }
        public string? Tags { get; set; }
        public string SearchDetails { get; set; } = null!;
        
        
        public virtual Color.Color? Color { get; set; }
       // public virtual PackageType.PackageType OuterPackage { get; set; } = null!;
        public virtual  SupplierInfo Supplier { get; set; } = null!;
        public virtual PackageType.PackageType UnitPackage { get; set; } = null!;
        public virtual ICollection<ProductProductGroupMapping> ProductProductGroups { get; set; }
        
       
    }
}
