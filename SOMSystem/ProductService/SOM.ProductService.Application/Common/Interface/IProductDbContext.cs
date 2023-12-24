using Microsoft.EntityFrameworkCore;
using SOM.ProductService.Domain.Color;
using SOM.ProductService.Domain.PackageType;
using SOM.ProductService.Domain.Product;
using SOM.ProductService.Domain.Supplier;
using SOM.Shared.Interfaces;

namespace SOM.ProductService.Application.Common.Interface
{
    public interface IProductDbContext: IDbContextBase
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<ProductProductGroupMapping> ProductProductGroupMappings { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<PackageType> PackageTypes { get; set; }
        public DbSet<Domain.Supplier.SupplierInfo> SupplierInfos { get; set; }
    }
}
