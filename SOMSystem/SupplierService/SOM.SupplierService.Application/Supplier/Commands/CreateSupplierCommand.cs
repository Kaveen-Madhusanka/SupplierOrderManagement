using AutoMapper;
using MediatR;
using SOM.SupplierService.Application.Common.Interface;
using SOM.SupplierService.Application.Supplier.Notifications;

namespace SOM.SupplierService.Application.Supplier.Commands
{
    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, int>
    {
        private readonly ISupplierDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateSupplierCommandHandler(ISupplierDbContext appDbContext, IMapper mapper, IMediator mediator)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<int> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = _mapper.Map<Domain.Supplier.Supplier>(request);   
            await _appDbContext.Suppliers.AddAsync(supplier, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(_mapper.Map<SupplierCreated>(supplier), cancellationToken);

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