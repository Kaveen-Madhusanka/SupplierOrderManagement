using Microsoft.EntityFrameworkCore;
using SOM.Shared.Interfaces;

namespace SOM.SupplierService.Application.Common.Interface
{
    public interface ISupplierDbContext: IDbContextBase
    {
        DbSet<Domain.Supplier.Supplier> Suppliers { get; set; }
    }
}
