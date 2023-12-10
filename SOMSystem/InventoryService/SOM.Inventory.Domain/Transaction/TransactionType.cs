using SOM.Shared.Entities;

namespace SOM.Inventory.Domain.Transaction;

public class TransactionType: AuditableEntity
{
    public int Id { get; set; }
    public string TransactionTypeName { get; set; } = null!;
}