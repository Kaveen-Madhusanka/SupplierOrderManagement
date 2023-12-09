using SOM.Shared.Entities;

namespace SOM.SupplierService.Domain.Supplier
{
    public class Supplier : AuditableEntity
    {
        public int Id { get; set; }
        public string SupplierName { get; set; } = null!;
        public int SupplierCategoryId { get; set; }
        public string? SupplierReference { get; set; }
        public string? BankAccountName { get; set; }
        public string? BankAccountBranch { get; set; }
        public string? BankAccountCode { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? BankInternationalCode { get; set; }
        public int PaymentDays { get; set; }
        public string? InternalComments { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string FaxNumber { get; set; } = null!;
        public string WebsiteUrl { get; set; } = null!;
        public string DeliveryAddressLine1 { get; set; } = null!;
        public string? DeliveryAddressLine2 { get; set; }
        public string DeliveryPostalCode { get; set; } = null!;
        public string PostalAddressLine1 { get; set; } = null!;
        public string? PostalAddressLine2 { get; set; }
        public string PostalPostalCode { get; set; } = null!;
        
        public virtual SupplierCategory SupplierCategory { get; set; } = null!;
        
        
        // add City from cache and find best way to join
    }
}