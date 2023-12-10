using SOM.Shared.Entities;

namespace SOM.SupplierService.Domain.Supplier;

public class SupplierCategory : AuditableEntity
{ 
    public SupplierCategory()
    {
        Suppliers = new HashSet<Supplier>();
    }
    
    public int Id { get; set; }
    public string SupplierCategoryName { get; set; } = null!;
    
    public virtual ICollection<Supplier> Suppliers { get; set; }
    
}