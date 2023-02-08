using AutoMapper;
using Portfolio.Dtos;
using Portfolio.Entities;

namespace Portfolio.Business.Mappings.AutoMapper
{
    public class ContentProfile : Profile
    {
        public ContentProfile()
        {
            CreateMap<Content, ContentCreateDto>().ReverseMap();
            CreateMap<Content, ContentUpdateDto>().ReverseMap();
            CreateMap<Content, ContentListDto>().ReverseMap();

        }
    }
}
