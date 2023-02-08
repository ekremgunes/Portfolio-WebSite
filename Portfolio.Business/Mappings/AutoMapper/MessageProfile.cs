using AutoMapper;
using Portfolio.Dtos;
using Portfolio.Entities;

namespace Portfolio.Business.Mappings.AutoMapper
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageListDto>().ReverseMap();
            CreateMap<Message, MessageCreateDto>().ReverseMap();
        }
    }
}
