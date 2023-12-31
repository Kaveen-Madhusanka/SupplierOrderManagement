﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using SOM.SupplierService.Application.Common.Interface;

namespace SOM.SupplierService.Application.Supplier.Queries
{
    public record GetSupplierQuery : IRequest<List<Domain.Supplier.Supplier>>;

    public class GetSupplierQueryHandler : IRequestHandler<GetSupplierQuery, List<Domain.Supplier.Supplier>>
    {
        private readonly ISupplierDbContext _appDbContext;
        public GetSupplierQueryHandler(ISupplierDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Domain.Supplier.Supplier>> Handle(GetSupplierQuery request, CancellationToken cancellationToken)
        {
            var supplier = await _appDbContext.Suppliers.ToListAsync(cancellationToken: cancellationToken);
            return supplier;
        }
    }
}
