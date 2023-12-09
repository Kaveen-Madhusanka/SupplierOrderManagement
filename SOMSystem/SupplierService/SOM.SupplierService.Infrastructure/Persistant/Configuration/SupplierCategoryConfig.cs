using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOM.SupplierService.Domain.Supplier;

namespace SOM.SupplierService.Infrastructure.Persistant.Configuration
{
    public class SupplierCategoryConfig : IEntityTypeConfiguration<SupplierCategory>
    {
        public void Configure(EntityTypeBuilder<SupplierCategory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SupplierCategoryName).IsRequired().HasMaxLength(500);
            builder.HasMany(x => x.Suppliers)
                .WithOne(b => b.SupplierCategory)
                .HasForeignKey(b => b.SupplierCategoryId);
        }
    }
}
