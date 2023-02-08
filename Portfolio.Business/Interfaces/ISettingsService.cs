using Portfolio.Common;
using Portfolio.Dtos;

namespace Portfolio.Business.Interfaces
{
    public interface ISettingsService 
    {
        Task<IResponse<SettingsUpdateDto>> UpdateSettingsAsync(SettingsUpdateDto dto);
        bool UpdateSettingsIsValid(SettingsUpdateDto dto);
        Task<IResponse<SettingsListDto>> GetSettingsAsync(int id);
        Task<IResponse<SettingsUpdateDto>> GetSettingsUpdateAsync(int id);
        Task<bool> TimeMessageIsActive(int id);
        Task addVisitor();
        Task addVisitorInteraction();
        Task updateSeo(string seo);

	}
}
