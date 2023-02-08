using AutoMapper;
using Portfolio.Dtos;
using Portfolio.Entities;

namespace Portfolio.Business.Mappings.AutoMapper
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<Service,ServiceCreateDto>().ReverseMap();
            CreateMap<Service,ServiceUpdateDto>().ReverseMap();
            CreateMap<Service,ServiceListDto>().ReverseMap();
        }
    }
}
