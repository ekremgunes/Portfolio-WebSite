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
    public class ImageService : IImageService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<ImageCreateDto> _imageCreateValidator;
        public ImageService(IUow uow, IMapper mapper, IValidator<ImageCreateDto> imageCreateValidator)
        {
            _uow = uow;
            _mapper = mapper;
            _imageCreateValidator = imageCreateValidator;
        }

        public async Task<IResponse<ImageCreateDto>> CreateImage(string path,int contentDetailId)
        {
            var dto = new ImageCreateDto()
            {
                imgPath= path,
                contentDetailId=contentDetailId
            };
            var result = _imageCreateValidator.Validate(dto);
            if (result.IsValid)
            {
                var createdEntity = _mapper.Map<Image>(dto);
                await _uow.GetRepository<Image>().CreateAsync(createdEntity);
                await _uow.SaveChangesAsync();
                return new Response<ImageCreateDto>(ResponseType.Success, dto);
            }
            return new Response<ImageCreateDto>(dto,result.ConvertToCustomValidationError());
        }

        public async Task<IResponse> DeleteImageAsync(int id)
        {
            var data = await _uow.GetRepository<Image>().FindAsync(id);
            if (data == null)
                return new Response(ResponseType.NotFound, $"{id} idsine sahip resim bulunamadı");
            _uow.GetRepository<Image>().Remove(data);
            await _uow.SaveChangesAsync();
            FileHelper.DeleteFile(data.imgPath);
            return new Response(ResponseType.Success);
        }
    }
}
