using AutoMapper;
using FluentValidation;
using Portfolio.Business.Extensions;
using Portfolio.Business.Interfaces;
using Portfolio.Common;
using Portfolio.DataAccess.UnitOfWork;
using Portfolio.Dtos;
using Portfolio.Entities;

namespace Portfolio.Business.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<SettingsUpdateDto> _settingsUpdateValidator;

        public SettingsService(IUow uow, IMapper mapper, IValidator<SettingsUpdateDto> settingsUpdateValidator)
        {
            _uow = uow;
            _mapper = mapper;
            _settingsUpdateValidator = settingsUpdateValidator;
        }

		public async Task addVisitor()
		{
			var Entity = await _uow.GetRepository<Settings>().FindAsync(1);
            Entity.visitorCount += 1;
            await _uow.SaveChangesAsync();
		}

		public async Task addVisitorInteraction()
		{
			var Entity = await _uow.GetRepository<Settings>().FindAsync(1);
			Entity.visitorInteraction += 1;
			await _uow.SaveChangesAsync();

		}

		public async Task<IResponse<SettingsListDto>> GetSettingsAsync(int id = 1)
        {
            var Entity = await _uow.GetRepository<Settings>().FindAsync(id);
            if (Entity == null)
            {
                return new Response<SettingsListDto>(ResponseType.NotFound, new SettingsListDto());
            }
            return new Response<SettingsListDto>(ResponseType.Success, _mapper.Map<SettingsListDto>(Entity));

        }

        public async Task<IResponse<SettingsUpdateDto>> GetSettingsUpdateAsync(int id = 1)
        {
            var Entity = await _uow.GetRepository<Settings>().FindAsync(id);
            if (Entity == null)
            {
                return new Response<SettingsUpdateDto>(ResponseType.NotFound, new SettingsUpdateDto());
            }
            return new Response<SettingsUpdateDto>(ResponseType.Success, _mapper.Map<SettingsUpdateDto>(Entity));
        }

        public async Task<bool> TimeMessageIsActive(int id = 1)
        {
            var Entity = await _uow.GetRepository<Settings>().FindAsync(id);
            if (Entity == null)
            {
                return true;
            }
            else
            {
                if (Entity.timedMessage == true)
                    return true;
                else
                    return false;
            }
        }

		public async Task updateSeo(string seo)
		{
			var Entity = await _uow.GetRepository<Settings>().FindAsync(1);
			Entity.seo = seo;
			await _uow.SaveChangesAsync();
		}

		public async Task<IResponse<SettingsUpdateDto>> UpdateSettingsAsync(SettingsUpdateDto dto)
        {
            var control = _settingsUpdateValidator.Validate(dto);
            if (control.IsValid)
            {
                var oldEntity = await _uow.GetRepository<Settings>().FindAsync(dto.id);
                if (oldEntity != null)
                {
                    _uow.GetRepository<Settings>().Update(_mapper.Map<Settings>(dto), oldEntity);
                    await _uow.SaveChangesAsync();
                    return new Response<SettingsUpdateDto>(ResponseType.Success, dto);
                }
                return new Response<SettingsUpdateDto>(ResponseType.NotFound, dto);

            }
            return new Response<SettingsUpdateDto>(dto, control.ConvertToCustomValidationError());

        }

        public bool UpdateSettingsIsValid(SettingsUpdateDto dto)
        {
            var control = _settingsUpdateValidator.Validate(dto);
            if (control.IsValid)
                return true;
            else
                return false;
        }
    }
}
