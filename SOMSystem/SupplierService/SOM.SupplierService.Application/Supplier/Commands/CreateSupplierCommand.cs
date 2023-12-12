using AutoMapper;
using MediatR;
using SOM.SupplierService.Application.Common.Interface;

namespace SOM.SupplierService.Application.Supplier.Commands
{
    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, int>
    {
        private readonly ISupplierDbContext _appDbContext;
        private readonly IMapper _mapper;

        public CreateSupplierCommandHandler(ISupplierDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = _mapper.Map<Domain.Supplier.Supplier>(request);   
            await _appDbContext.Suppliers.AddAsync(supplier, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return supplier.Id;
        }
    }

    public class CreateSupplierCommand : IRequest<int>
    {
        public string SupplierName { get; set; } = null!;
        public int SupplierCategoryId { get; set; }
        public int PaymentDays { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string FaxNumber { get; set; } = null!;
        public string WebsiteUrl { get; set; } = null!;
        public string DeliveryAddressLine1 { get; set; } = null!;
        public string DeliveryPostalCode { get; set; } = null!;
        public string PostalAddressLine1 { get; set; } = null!;
        public string PostalPostalCode { get; set; } = null!;
    }
}