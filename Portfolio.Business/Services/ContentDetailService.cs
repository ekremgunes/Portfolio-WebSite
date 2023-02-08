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
    public class ContentDetailService : Service<ContentDetailCreateDto,
            ContentDetailUpdateDto,
            ContentDetailListDto,
            ContentDetail>,
            IContentDetailService
    {
        private readonly IMapper _mapper;
        private readonly IUow _uow;
        private readonly IValidator<ContentDetailCreateDto> _createDtoValidator;
        private readonly IValidator<ContentDetailUpdateDto> _updateDtoValidator;
        public ContentDetailService(IMapper mapper, IUow uow, IValidator<ContentDetailCreateDto> createDtoValidator, IValidator<ContentDetailUpdateDto> updateDtoValidator) : base(mapper, uow, createDtoValidator, updateDtoValidator)
        {
            _mapper = mapper;
            _uow = uow;
            _createDtoValidator = createDtoValidator;
            _updateDtoValidator = updateDtoValidator;
        }
        public IResponse<List<ContentDetailListDto>> GetAllDetailsWithContent()
        {
            var query = _uow.GetRepository<ContentDetail>().GetQuery();
            var data = query.Include(x => x.content).ToList();
            var dto = _mapper.Map<List<ContentDetailListDto>>(data);
            if (data == null)
            {
                return new Response<List<ContentDetailListDto>>(ResponseType.NotFound, dto);
            }
            return new Response<List<ContentDetailListDto>>(ResponseType.Success, dto);
        }


        public int LastContentDetailId()
        {
            return _uow.GetRepository<ContentDetail>().GetQuery().Max(x => x.id);
        }

        public async Task<IResponse> RemoveContentDetailAsync(int id)
        {
            var data = await _uow.GetRepository<ContentDetail>().GetQuery().Where(x => x.id == id).Include("images").FirstOrDefaultAsync();
            if (data == null)
                return new Response(ResponseType.NotFound, $"{id} idsine sahip resim bulunamadı");
            _uow.GetRepository<ContentDetail>().Remove(data);
            await _uow.SaveChangesAsync();
            //remove files
            FileHelper.DeleteFile(data.imagePath);
            if (data.images.Count > 0)
            {
                foreach (var img in data.images)
                {
                    FileHelper.DeleteFile(img.imgPath);
                }
            }
            return new Response(ResponseType.Success);
        }

        public async Task<IResponse<ContentDetailListDto>> GetDetailwithImagesAsync(int contentDetailId)
        {
            var data = await _uow.GetRepository<ContentDetail>().GetQuery().Where(x => x.id == contentDetailId).Include(x => x.images).FirstOrDefaultAsync();
            if (data == null)
                return new Response<ContentDetailListDto>(ResponseType.NotFound, $"{contentDetailId} data can't fonuned");
            var dto = _mapper.Map<ContentDetailListDto>(data);
            return new Response<ContentDetailListDto>(ResponseType.Success, dto);
        }
        public bool ContentDetailIsValid(ContentDetailCreateDto dto)
        {
            return _createDtoValidator.Validate(dto).IsValid ? true : false;
        }
        public bool ContentDetailUpdateIsValid(ContentDetailUpdateDto dto)
        {
            return _updateDtoValidator.Validate(dto).IsValid ? true : false;
        }

        public async Task<List<ContentDetailListDto>> GetAllDetailsbyRankAsync()
        {
            var data = await _uow.GetRepository<ContentDetail>().GetAllAsync(x => x.rank,Common.Enums.OrderByType.ASC);
            return _mapper.Map<List<ContentDetailListDto>>(data);
        }

        public async Task<ContentDetailListDto> getContentOfDetail(int detailId)
        {
            var data = await _uow.GetRepository<ContentDetail>().GetQuery().Where(x=>x.id == detailId).Include(x => x.content).FirstOrDefaultAsync();
            return _mapper.Map<ContentDetailListDto>(data);

        }

        public async Task<int> contentDetailCount()
        {
            return await _uow.GetRepository<ContentDetail>().getCountAsync();
        }
    }
}
