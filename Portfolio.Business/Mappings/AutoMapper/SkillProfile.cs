using AutoMapper;
using Portfolio.Dtos;
using Portfolio.Entities;

namespace Portfolio.Business.Mappings.AutoMapper
{
    public class SkillProfile : Profile
    {
        public SkillProfile()
        {
            CreateMap<Skill,SkillCreateDto>().ReverseMap();
            CreateMap<Skill,SkillUpdateDto>().ReverseMap();
            CreateMap<Skill,SkillListDto>().ReverseMap();
        }
    }
}
