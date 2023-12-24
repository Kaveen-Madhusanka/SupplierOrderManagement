using MediatR;
using SOM.ProductService.Application.Common.Interface;
using SOM.ProductService.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace SOM.ProductService.Application.Products.Commands
{
    public record CreateProductCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int SupplierId { get; set; }
        public int? ColorId { get; set; }
        public int UnitPackageId { get; set; }
        //public int OuterPackageId { get; set; }
        public string? Brand { get; set; }
        public string? Size { get; set; }
        public int LeadTimeDays { get; set; }
        public int QuantityPerOuter { get; set; }
        public bool IsChillerStock { get; set; }
        public string? Barcode { get; set; }
        public decimal TaxRate { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? RecommendedRetailPrice { get; set; }
        public decimal TypicalWeightPerUnit { get; set; }
        public string? MarketingComments { get; set; }
        public string? InternalComments { get; set; }
        public byte[]? Photo { get; set; }
        public string? CustomFields { get; set; }
        public string? Tags { get; set; }
        public string SearchDetails { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductDbContext _appDbContext;
        private readonly IMapper _mapper;
        
        public CreateProductCommandHandler(IProductDbContext appDbContext,
            IMapper mapper)
        {
            _appDbContext= appDbContext;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            await _appDbContext.Products.AddAsync(product, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return product.Id;
        }
    }
}
