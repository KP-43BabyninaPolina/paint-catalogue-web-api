using AutoMapper;
using BrandEntity = PaintCatalogue_WebAPI.DataModel.Entities.Brand;

namespace PaintCatalogue_WebAPI.Dtos.Brand;

public class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<BrandCreateDto, BrandEntity>();

        CreateMap<BrandEntity, BrandOutputDto>();
        
        CreateMap<BrandEntity, BrandShortOutputDto>();
    }
}