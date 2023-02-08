using Portfolio.Common;
using Portfolio.Dtos;
using Portfolio.Entities;

namespace Portfolio.Business.Interfaces
{
    public interface IContentDetailService : IService<ContentDetailCreateDto,ContentDetailUpdateDto,ContentDetailListDto,ContentDetail>
    {
        Task<int> contentDetailCount();
        Task<ContentDetailListDto> getContentOfDetail(int detailId);
        int LastContentDetailId();
        bool ContentDetailIsValid(ContentDetailCreateDto dto);
        bool ContentDetailUpdateIsValid(ContentDetailUpdateDto dto);
        IResponse<List<ContentDetailListDto>> GetAllDetailsWithContent();
        Task<IResponse<ContentDetailListDto>> GetDetailwithImagesAsync(int contentDetailId);
        Task<IResponse> RemoveContentDetailAsync(int id);
        Task<List<ContentDetailListDto>> GetAllDetailsbyRankAsync();

    }
}
