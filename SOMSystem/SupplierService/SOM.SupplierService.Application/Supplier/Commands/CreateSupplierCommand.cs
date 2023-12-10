using MediatR;
using SOM.SupplierService.Application.Common.Interface;

namespace SOM.SupplierService.Application.Supplier.Commands
{
    public record CreateSupplierCommand(Domain.Supplier.Supplier Supplier) : IRequest<int>;

    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, int>
    {
        private readonly ISupplierDbContext _appDbContext;
        public CreateSupplierCommandHandler(ISupplierDbContext appDbContext)
        {
            _appDbContext= appDbContext;
        }
        public async Task<int> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            await _appDbContext.Suppliers.AddAsync(request.Supplier, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return request.Supplier.Id;
        }
    }
}
