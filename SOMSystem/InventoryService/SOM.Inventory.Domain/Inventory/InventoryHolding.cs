using SOM.Inventory.Domain.Product;
using SOM.Shared.Entities;

namespace SOM.Inventory.Domain.Inventory;

public class InventoryHolding: AuditableEntity
{
    public int Id { get; set; }
    public int  ProductId { get; set; }
    public int QuantityOnHand { get; set; }
    public string BinLocation { get; set; } = null!;
    public int LastStockTakeQuantity { get; set; }
    public decimal LastCostPrice { get; set; }
    public int ReorderLevel { get; set; }
    public int TargetStockLevel { get; set; }

    public virtual ProductInfo Product { get; set; } = null!;
}