﻿using Microsoft.EntityFrameworkCore;
using SOM.ProductService.Application.Common.Interface;
using SOM.ProductService.Domain.Color;
using SOM.ProductService.Domain.PackageType;
using SOM.ProductService.Domain.Product;
using SOM.ProductService.Domain.Supplier;
using SOM.Shared.Entities;
using System.Reflection;

namespace SOM.ProductService.Infrastructure.Persistant
{
    public class ProductDbContext : DbContext, IProductDbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<ProductProductGroupMapping> ProductProductGroupMappings { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<PackageType> PackageTypes { get; set; }
        public DbSet<SupplierInfo> SupplierInfos { get; set; }
        

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
