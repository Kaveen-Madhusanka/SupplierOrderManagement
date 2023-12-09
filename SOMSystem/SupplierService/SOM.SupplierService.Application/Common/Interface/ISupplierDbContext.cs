using Microsoft.EntityFrameworkCore;
using SOM.Shared.Interfaces;
using SOM.SupplierService.Domain.Supplier;

namespace SOM.SupplierService.Application.Common.Interface
{
    public interface ISupplierDbContext: IDbContextBase
    {
        DbSet<Supplier> Suppliers { get; set; }
    }
}
