using AutoMapper;
using MediatR;
using SOM.ProductService.Application.Common.Interface;
using SOM.ProductService.Application.Products.Commands;
using SOM.ProductService.Domain.Product;

namespace SOM.ProductService.Application.SupplierInfo.Commands;

public class CreateSupplierInfoCommand: IRequest<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
}


public class CreateSupplierInfoCommandHandler : IRequestHandler<CreateSupplierInfoCommand, int>
{
    private readonly IProductDbContext _appDbContext;
    private readonly IMapper _mapper;
        
    public CreateSupplierInfoCommandHandler(IProductDbContext appDbContext,
        IMapper mapper)
    {
        _appDbContext= appDbContext;
        _mapper = mapper;
    }
    
    public async Task<int> Handle(CreateSupplierInfoCommand request, CancellationToken cancellationToken)
    {
        var supplierInfo = _mapper.Map<Domain.Supplier.SupplierInfo>(request);
        await _appDbContext.SupplierInfos.AddAsync(supplierInfo, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        return supplierInfo.Id;
    }
}
