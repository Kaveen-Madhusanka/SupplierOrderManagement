using AutoMapper;
using SOM.SupplierService.Application.Supplier.Commands;

namespace SOM.SupplierService.Application.Mappers;

public class SupplierMapperProfile: Profile
{
    public SupplierMapperProfile()
    {
        CreateMap<CreateSupplierCommand, Domain.Supplier.Supplier>();
    }
}