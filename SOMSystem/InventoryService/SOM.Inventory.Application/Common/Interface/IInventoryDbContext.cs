using Microsoft.EntityFrameworkCore;
using SOM.Inventory.Domain.Inventory;
using SOM.Inventory.Domain.Product;
using SOM.Inventory.Domain.Supplier;
using SOM.Inventory.Domain.Transaction;
using SOM.Shared.Interfaces;

namespace SOM.Inventory.Application.Common.Interface
{
    public interface IInventoryDbContext: IDbContextBase
    {
        public DbSet<SupplierInfo> SupplierInfos { get; set; }
        public DbSet<ProductInfo> ProductInfos { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<InventoryHolding> InventoryHoldings { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
    }
}
