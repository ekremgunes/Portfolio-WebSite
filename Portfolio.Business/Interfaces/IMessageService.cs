using Portfolio.Common;
using Portfolio.Dtos;

namespace Portfolio.Business.Interfaces
{
    public interface IMessageService
    {
        Task<IResponse<MessageCreateDto>> SendMessage(MessageCreateDto dto);
        Task<IResponse> DeleteMessage(int id);
        Task<IResponse> DeleteRangeMessagesAsync(int[] ids);
        Task<IResponse> isRead(int id,bool read = true);
        Task<IResponse<List<MessageListDto>>> GetAllMessagesAsync();
        Task<IResponse<List<MessageListDto>>> GetAllMessagesAsync(int month);
        Task<IResponse<List<MessageListDto>>> GetLastMessagesAsync(int unit = 4);
        Task<int> GetMessageCountAsync();


    }
}
