using AutoMapper;
using Portfolio.Dtos;
using Portfolio.Entities;

namespace Portfolio.Business.Mappings.AutoMapper
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<Image,ImageCreateDto>().ReverseMap();
            CreateMap<Image,ImageListDto>().ReverseMap();
        }
    }
}
