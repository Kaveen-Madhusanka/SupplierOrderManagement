using MediatR;
using Microsoft.EntityFrameworkCore;
using SOM.SupplierService.Application.Common.Interface;
using SOM.SupplierService.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOM.SupplierService.Application.Products.Queries
{
    public record GetProductQuery : IRequest<List<Supplier>>;

    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, List<Supplier>>
    {
        private readonly ISupplierDbContext _appDbContext;
        public GetProductQueryHandler(ISupplierDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Supplier>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _appDbContext.Suppliers.ToListAsync();

                return products;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
