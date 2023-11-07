﻿using MediatR;
using SOM.ProductService.Application.Common.Interface;
using SOM.ProductService.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOM.ProductService.Application.Products.Commands
{
    public record CreateProductCommand(Product Product) : IRequest<int>;

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IAppDbContext _appDbContext;
        public CreateProductCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext= appDbContext;
        }
        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _appDbContext.Products.AddAsync(request.Product, cancellationToken);
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