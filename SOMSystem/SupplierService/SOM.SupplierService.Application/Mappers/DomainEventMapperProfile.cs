using AutoMapper;
using SOM.SupplierService.Application.Supplier.Notifications;

namespace SOM.SupplierService.Application.Mappers;

public class DomainEventMapperProfile: Profile
{
    public DomainEventMapperProfile()
    {
        CreateMap<Domain.Supplier.Supplier, SupplierCreated>();
    }
}