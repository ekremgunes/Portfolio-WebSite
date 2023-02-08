using Portfolio.Common;
using Portfolio.Dtos;

namespace Portfolio.Business.Interfaces
{
    public interface IImageService 
    {

        Task<IResponse<ImageCreateDto>> CreateImage(string path, int contentDetailId);
        Task<IResponse> DeleteImageAsync(int id);

    }
}
