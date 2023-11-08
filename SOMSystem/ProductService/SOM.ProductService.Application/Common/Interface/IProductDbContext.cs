using Microsoft.EntityFrameworkCore;
using SOM.ProductService.Domain.Product;
using SOM.Shared.Interfaces;

namespace SOM.ProductService.Application.Common.Interface
{
    public interface IProductDbContext: IDbContextBase
    {
        DbSet<Product> Products { get; set; }
        DbSet<ProductCategory> ProductCategories { get; set; }
    }
}
