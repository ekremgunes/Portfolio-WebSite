using Portfolio.Common;
using Portfolio.Dtos;
using Portfolio.Entities;

namespace Portfolio.Business.Interfaces
{
    public interface ISkillService : IService<SkillCreateDto,SkillUpdateDto,SkillListDto,Skill>
    {
        Task<IResponse<List<SkillListDto>>> GetAllSkillbyRankAsync();

    }

}
