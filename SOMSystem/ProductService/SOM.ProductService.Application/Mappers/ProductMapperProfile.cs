using AutoMapper;
using SOM.ProductService.Application.Products.Commands;
using SOM.ProductService.Application.SupplierInfo.Commands;
using SOM.ProductService.Domain.Product;

namespace SOM.ProductService.Application.Mappers;

public class ProductMapperProfile : Profile
{
    public ProductMapperProfile()
    {
        CreateMap<CreateProductCommand, Product>();
        CreateMap<CreateSupplierInfoCommand, Domain.Supplier.SupplierInfo>();
    }
}