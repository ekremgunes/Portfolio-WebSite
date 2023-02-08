using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Portfolio.Business.Extensions;
using Portfolio.Business.Interfaces;
using Portfolio.Common;
using Portfolio.Common.Enums;
using Portfolio.DataAccess.UnitOfWork;
using Portfolio.Dtos;
using Portfolio.Entities;

namespace Portfolio.Business.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<MessageCreateDto> _createMessageValidator;

        public MessageService(IUow uow, IMapper mapper, IValidator<MessageCreateDto> createMessageValidator)
        {
            _uow = uow;
            _mapper = mapper;
            _createMessageValidator = createMessageValidator;
        }

        public async Task<IResponse> DeleteMessage(int id)
        {
            var entity = await _uow.GetRepository<Message>().FindAsync(id);
            if (entity ==null)
                return new Response(ResponseType.NotFound);
            
            _uow.GetRepository<Message>().Remove(entity);
            await _uow.SaveChangesAsync(); 
            return new Response(ResponseType.Success);

        }

        public async Task<IResponse> DeleteRangeMessagesAsync(int[] ids)
        {
            return new Response(ResponseType.ValidationError);

        }

        public async Task<IResponse<List<MessageListDto>>> GetAllMessagesAsync()
        {
            var data = await _uow.GetRepository<Message>().GetAllAsync(x=>x.date,OrderByType.DESC);
            var messages = _mapper.Map<List<MessageListDto>>(data);

            if (messages != null)
                return new Response<List<MessageListDto>>(ResponseType.Success, messages);
            else
                return new Response<List<MessageListDto>>(ResponseType.NotFound, messages);

        }

        public async Task<IResponse> isRead(int id, bool read=true)
        {
            var entity = await _uow.GetRepository<Message>().FindAsync(id);
            if (entity == null)

                return new Response(ResponseType.NotFound);

            entity.isRead = read;
            await _uow.SaveChangesAsync();
            return new Response(ResponseType.Success);
        }

        public async Task<IResponse<MessageCreateDto>> SendMessage(MessageCreateDto dto)
        {
            var result = _createMessageValidator.Validate(dto);
            if (!result.IsValid)
            {
                return new Response<MessageCreateDto>(dto, result.ConvertToCustomValidationError());
            }
            var createdMessage = _mapper.Map<Message>(dto);
            await _uow.GetRepository<Message>().CreateAsync(createdMessage);
            await _uow.SaveChangesAsync();
            return new Response<MessageCreateDto>(ResponseType.Success,dto);

        }

        public async Task<IResponse<List<MessageListDto>>> GetLastMessagesAsync(int unit = 4)
        {
            var query = _uow.GetRepository<Message>().GetQuery();
            var data = await query.Where(x=>x.isRead==false).OrderByDescending(x=>x.date).Take(unit).AsNoTracking().ToListAsync();
            var messages = _mapper.Map<List<MessageListDto>>(data);

            if (messages.Count > 0)
                return new Response<List<MessageListDto>>(ResponseType.Success, messages);
            else
                return new Response<List<MessageListDto>>(ResponseType.NotFound, messages);

        }

        public async Task<int> GetMessageCountAsync()
        {
            var query =  _uow.GetRepository<Message>().GetQuery();
            return await query.CountAsync(x => x.isRead == false);
        }

        public async Task<IResponse<List<MessageListDto>>> GetAllMessagesAsync(int month)
        {
            var query = _uow.GetRepository<Message>().GetQuery();
            var data = await query.Where(x => x.date.AddMonths(month) >= DateTime.Now).OrderByDescending(x=>x.date).AsNoTracking().ToListAsync();
            var messages = _mapper.Map<List<MessageListDto>>(data);
            if (messages.Count <= 0)
            {
                return new Response<List<MessageListDto>>(ResponseType.NotFound, messages);
            }
            var deletedMessages = await query.Where(x => x.date.AddMonths(month) < DateTime.Now).AsNoTracking().ToListAsync();
            if(deletedMessages.Count > 0)
            {
                _uow.GetRepository<Message>().RemoveRange(deletedMessages);
                await _uow.SaveChangesAsync();
            }
            return new Response<List<MessageListDto>>(ResponseType.Success, messages);
        }
    }
}
