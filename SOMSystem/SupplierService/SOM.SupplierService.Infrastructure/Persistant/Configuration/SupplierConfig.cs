using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOM.SupplierService.Domain.Supplier;

namespace SOM.SupplierService.Infrastructure.Persistant.Configuration
{
    public class SupplierConfig : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SupplierName).IsRequired().HasMaxLength(500);
            builder.Property(x => x.PostalPostalCode).IsRequired().HasMaxLength(255);
            builder.Property(x => x.PostalAddressLine1).IsRequired().HasMaxLength(500);
            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(100);
            
        }
    }
}
