using Microsoft.EntityFrameworkCore;
using SOM.Shared.Interfaces;
using SOM.SupplierService.Domain.Supplier;

namespace SOM.SupplierService.Application.Common.Interface
{
    public interface ISupplierDbContext: IDbContextBase
    {
        DbSet<Domain.Supplier.Supplier> Suppliers { get; set; }
        public DbSet<SupplierCategory> SupplierCategories { get; set; }
    }
}
