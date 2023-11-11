using MediatR;
using SOM.SupplierService.Application.Common.Interface;
using SOM.SupplierService.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOM.SupplierService.Application.Products.Commands
{
    public record CreateProductCommand(Supplier Product) : IRequest<int>;

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly ISupplierDbContext _appDbContext;
        public CreateProductCommandHandler(ISupplierDbContext appDbContext)
        {
            _appDbContext= appDbContext;
        }
        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _appDbContext.Suppliers.AddAsync(request.Product, cancellationToken);
                await _appDbContext.SaveChangesAsync(cancellationToken);
                return request.Product.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
