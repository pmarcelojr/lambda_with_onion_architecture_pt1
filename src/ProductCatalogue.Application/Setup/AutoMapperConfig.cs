using AutoMapper;
using ProductCatalogue.Application.Dtos;
using ProductCatalogue.Domain.Products;

namespace ProductCatalogue.Application.Setup;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        this.CreateMap<Product, ProductDto>()
            .ForMember(x => x.Price, cfg => cfg.MapFrom(s => s.Price.Value));
    }
}
