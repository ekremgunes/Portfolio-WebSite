namespace Portfolio.Entities
{
    public class Message : BaseEntity
    {
        public string senderName { get; set; }
        public string senderMail { get; set; }
        public string messageContent { get; set; }
        public bool? isRead { get; set; }
        public DateTime  date { get; set; }
    }
}
