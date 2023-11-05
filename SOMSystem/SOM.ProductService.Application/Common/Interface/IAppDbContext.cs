using Microsoft.EntityFrameworkCore;
using SOM.ProductService.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOM.ProductService.Application.Common.Interface
{
    public interface IAppDbContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<ProductCategory> ProductCategories { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
    }
}
