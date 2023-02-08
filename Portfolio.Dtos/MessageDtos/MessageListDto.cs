using Portfolio.Dtos.Interfaces;

namespace Portfolio.Dtos
{
    public class MessageListDto : IDto
    {
        public int id { get; set; }
        public string senderName { get; set; }
        public string senderMail { get; set; }
        public string messageContent { get; set; }
        public bool isRead { get; set; }
        public DateTime  date { get; set; }
    }
}
