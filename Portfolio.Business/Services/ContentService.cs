using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Portfolio.Business.Interfaces;
using Portfolio.Common;
using Portfolio.DataAccess.UnitOfWork;
using Portfolio.Dtos;
using Portfolio.Entities;

namespace Portfolio.Business.Services
{
    public class ContentService : Service<ContentCreateDto, ContentUpdateDto, ContentListDto, Content>, IContentService
    {
        private readonly IMapper _mapper;
        private readonly IUow _uow;
        private readonly IValidator<ContentCreateDto> _createContentValidator;
        private readonly IValidator<ContentUpdateDto> _updateContentValidator;

        public ContentService(IMapper mapper, IUow uow, IValidator<ContentCreateDto> createDtoValidator, IValidator<ContentUpdateDto> updateDtoValidator, IValidator<ContentUpdateDto> updateContentValidator) : base(mapper, uow, createDtoValidator, updateDtoValidator)
        {
            _mapper = mapper;
            _uow = uow;
            _createContentValidator = createDtoValidator;
            _updateContentValidator = updateContentValidator;
        }

        public async Task<int> contentCount()
        {
            return await _uow.GetRepository<Content>().getCountAsync();
        }

        public bool ContentIsValid(ContentCreateDto dto) => _createContentValidator.Validate(dto).IsValid;


        public bool ContentUpdateIsValid(ContentUpdateDto dto) => _updateContentValidator.Validate(dto).IsValid;

        public async Task<List<ContentListDto>> GetContentsbyRankAsync()
        {
            var data = await _uow.GetRepository<Content>().GetAllAsync(x => x.rank,Common.Enums.OrderByType.ASC);
            return _mapper.Map<List<ContentListDto>>(data);
        }

        public async Task<IResponse<ContentListDto>> GetContentwithDetailsAsync(int id)
        {
            var data = await _uow.GetRepository<Content>().GetQuery().Where(x => x.id == id).OrderBy(x=>x.rank).Include(x => x.Details).ThenInclude(x=>x.images).FirstOrDefaultAsync();
            if (data == null)
                return new Response<ContentListDto>(ResponseType.NotFound,$"{id} data can't fonuned");
            var dto = _mapper.Map<ContentListDto>(data);
            return new Response<ContentListDto>(ResponseType.Success, dto);
        }

		public async Task<IResponse<List<ContentListDto>>> GetShowcaseContents()
		{
			var data = await _uow.GetRepository<Content>().GetQuery().Where(x => x.InShowCase == true).OrderBy(x=>x.rank).ToListAsync();
			if (data == null)
				return new Response<List<ContentListDto>>(ResponseType.NotFound, "data can't fonuned");
			var dto = _mapper.Map<List<ContentListDto>>(data);
			return new Response<List<ContentListDto>>(ResponseType.Success, dto);
		}

		public async Task<IResponse> RemoveContentAsync(int id)
        {
            var data = await _uow.GetRepository<Content>().GetQuery().Where(x => x.id == id).Include(x=>x.Details).ThenInclude(x=>x.images).FirstOrDefaultAsync();
            if (data == null)
                return new Response(ResponseType.NotFound, $"{id} idsine sahip resim bulunamadı");

            _uow.GetRepository<Content>().Remove(data);
            await _uow.SaveChangesAsync();

            //remove files physical 
            FileHelper.DeleteFile(data.imagePath); //content img
            if (data.Details.Count > 0)
            {
                foreach (var detail in data.Details)
                {
                    FileHelper.DeleteFile(detail.imagePath); //detail image
                    if (detail.images.Count > 0)
                    {
                        foreach (var img in detail.images) // detail imageS 
                        {
                            FileHelper.DeleteFile(img.imgPath); 
                        }
                    }
                }
            }
            return new Response(ResponseType.Success);
        }
    }
}
