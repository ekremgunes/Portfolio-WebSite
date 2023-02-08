using AutoMapper;
using FluentValidation;
using Portfolio.Business.Interfaces;
using Portfolio.Common;
using Portfolio.Common.Enums;
using Portfolio.DataAccess.UnitOfWork;
using Portfolio.Dtos;
using Portfolio.Entities;

namespace Portfolio.Business.Services
{
    public class SkillService : Service<SkillCreateDto,SkillUpdateDto,SkillListDto,Skill>,ISkillService
    {
        private readonly IMapper _mapper;
        private readonly IUow _uow;
        public SkillService(IUow uow, IMapper mapper, IValidator<SkillCreateDto> createDtoValidator, IValidator<SkillUpdateDto> updateDtoValidator) : base(mapper,uow,createDtoValidator,updateDtoValidator)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IResponse<List<SkillListDto>>> GetAllSkillbyRankAsync()
        {
            var data = await _uow.GetRepository<Skill>().GetAllAsync(x => x.rank, OrderByType.ASC);
            var skills = _mapper.Map<List<SkillListDto>>(data);
            return new Response<List<SkillListDto>>(ResponseType.Success, skills);            
        }
    }
}
