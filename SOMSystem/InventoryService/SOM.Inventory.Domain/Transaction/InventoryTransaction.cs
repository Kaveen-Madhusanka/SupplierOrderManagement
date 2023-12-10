using SOM.Inventory.Domain.Product;
using SOM.Inventory.Domain.Supplier;
using SOM.Shared.Entities;

namespace SOM.Inventory.Domain.Transaction;

public class InventoryTransaction: AuditableEntity
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int TransactionTypeId { get; set; }
    public int? SupplierId { get; set; }
    //public int? PurchaseOrderId { get; set; }
    public decimal Quantity { get; set; }
    
    public virtual ProductInfo Product { get; set; } = null!;
    public virtual SupplierInfo? Supplier { get; set; }
    public virtual TransactionType TransactionType { get; set; } = null!;
}