using Portfolio.Common;
using Portfolio.Dtos;
using Portfolio.Entities;

namespace Portfolio.Business.Interfaces
{
    public interface IServicesService : IService<ServiceCreateDto,ServiceUpdateDto,ServiceListDto,Service>
    {
        Task<IResponse<List<ServiceListDto>>> GetAllbyRankAsync();
    }
    

}
