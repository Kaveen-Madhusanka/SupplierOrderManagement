using Microsoft.EntityFrameworkCore;
using SOM.SupplierService.Domain.Product;
using SOM.Shared.Interfaces;

namespace SOM.SupplierService.Application.Common.Interface
{
    public interface ISupplierDbContext: IDbContextBase
    {
        DbSet<Supplier> Suppliers { get; set; }
    }
}
