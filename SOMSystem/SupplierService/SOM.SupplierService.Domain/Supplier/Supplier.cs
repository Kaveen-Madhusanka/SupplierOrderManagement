

using SOM.Shared.Entities;

namespace SOM.SupplierService.Domain.Supplier
{
    public class Supplier : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
    }
}
