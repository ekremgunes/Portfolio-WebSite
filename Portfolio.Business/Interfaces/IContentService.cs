using Portfolio.Common;
using Portfolio.Dtos;
using Portfolio.Entities;

namespace Portfolio.Business.Interfaces
{
    public interface IContentService : IService<ContentCreateDto,ContentUpdateDto,ContentListDto,Content>
    {
        Task<int> contentCount();
        bool ContentIsValid(ContentCreateDto dto);
        bool ContentUpdateIsValid(ContentUpdateDto dto);
        Task<List<ContentListDto>> GetContentsbyRankAsync();
        Task<IResponse> RemoveContentAsync(int id);
        Task<IResponse<ContentListDto>> GetContentwithDetailsAsync(int id);
        Task<IResponse<List<ContentListDto>>> GetShowcaseContents();
	}
}
