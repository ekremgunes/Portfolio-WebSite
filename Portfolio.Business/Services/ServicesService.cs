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
    public class ServicesService : Service<ServiceCreateDto, ServiceUpdateDto, ServiceListDto, Service>, IServicesService
    {
        private readonly IMapper _mapper;
        private readonly IUow _uow;
        public ServicesService(IMapper mapper, IUow uow, IValidator<ServiceCreateDto> createDtoValidator, IValidator<ServiceUpdateDto> updateDtoValidator) : base(mapper, uow, createDtoValidator, updateDtoValidator)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<IResponse<List<ServiceListDto>>> GetAllbyRankAsync()
        {
            var data = await _uow.GetRepository<Service>().GetAllAsync(x=>x.rank, OrderByType.ASC);
            var dto = _mapper.Map<List<ServiceListDto>>(data);
            return new Response<List<ServiceListDto>>(ResponseType.Success, dto);
        }
    }
}
