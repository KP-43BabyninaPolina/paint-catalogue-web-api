using AutoMapper;
using PaintEntity = PaintCatalogue_WebAPI.DataModel.Entities.Paint;

namespace PaintCatalogue_WebAPI.Dtos.Paint;

public class PaintProfile : Profile
{
    public PaintProfile()
    {
        CreateMap<PaintCreateDto, PaintEntity>();

        CreateMap<PaintEntity, PaintOutputDto>()
            .ForMember(dest => dest.FullTitle, opt => opt.MapFrom(src => 
                $"{src.ColorName} {src.Type} {src.Brand.Name} {src.Volume}ml"));
    }
}