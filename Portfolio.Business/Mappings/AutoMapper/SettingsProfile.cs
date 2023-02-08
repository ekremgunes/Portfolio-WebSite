using AutoMapper;
using Portfolio.Dtos;
using Portfolio.Entities;

namespace Portfolio.Business.Mappings.AutoMapper
{
    public class SettingsProfile : Profile
    {
        public SettingsProfile()
        {
            CreateMap<Settings,SettingsUpdateDto>().ReverseMap();
            CreateMap<Settings,SettingsListDto>().ReverseMap();
        }
    }
}
