using AutoMapper;
using Portfolio.Dtos;
using Portfolio.Entities;

namespace Portfolio.Business.Mappings.AutoMapper
{
    public class ContentDetailProfile : Profile
    {
        public ContentDetailProfile()
        {
            CreateMap<ContentDetail,ContentDetailCreateDto>().ReverseMap();
            CreateMap<ContentDetail,ContentDetailUpdateDto>().ReverseMap();
            CreateMap<ContentDetail,ContentDetailListDto>().ReverseMap();
        }
    }
}
