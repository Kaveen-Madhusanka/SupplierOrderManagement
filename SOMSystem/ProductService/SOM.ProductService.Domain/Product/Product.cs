using SOM.Shared.Common;

namespace SOM.ProductService.Domain.Product
{
    public class Product : AuditableEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public decimal? UnitPrice { get; set; }
        public string? Description { get; set; }
        public int? ProductCategoryId { get; set; }
        public ProductCategory? ProductCategory { get; set; } = null!;
    }
}
