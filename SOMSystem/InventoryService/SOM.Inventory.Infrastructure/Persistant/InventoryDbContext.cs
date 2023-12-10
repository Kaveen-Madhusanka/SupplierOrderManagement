using Microsoft.EntityFrameworkCore;
using SOM.Inventory.Application.Common.Interface;
using SOM.Shared.Entities;
using System.Reflection;
using SOM.Inventory.Domain.Inventory;
using SOM.Inventory.Domain.Product;
using SOM.Inventory.Domain.Supplier;
using SOM.Inventory.Domain.Transaction;

namespace SOM.Inventory.Infrastructure.Persistant
{
    public class InventoryDbContext : DbContext, IInventoryDbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {

        }
        
        public DbSet<SupplierInfo> Suppliers { get; set; }
        public DbSet<ProductInfo> Products { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<InventoryHolding> InventoryHoldings { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = "Kaveen";
                        entry.Entity.Created = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = "Kaveen";
                        entry.Entity.LastModified = DateTime.UtcNow;
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }  

            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
