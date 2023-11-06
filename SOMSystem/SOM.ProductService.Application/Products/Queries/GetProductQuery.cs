using MediatR;
using Microsoft.EntityFrameworkCore;
using SOM.ProductService.Application.Common.Interface;
using SOM.ProductService.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOM.ProductService.Application.Products.Queries
{
    public record GetProductQuery : IRequest<List<Product>>;

    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, List<Product>>
    {
        private readonly IAppDbContext _appDbContext;
        public GetProductQueryHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Product>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _appDbContext.Products.ToListAsync();

                return products;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
