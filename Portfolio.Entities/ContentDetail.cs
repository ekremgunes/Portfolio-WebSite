namespace Portfolio.Entities
{
    public class ContentDetail : BaseEntity
    {
        public string contentDetailName { get; set; }
        public string imagePath { get; set; }
        public short rank { get; set; }

        public int contentId { get; set; }
        public Content content { get; set; }
        public List<Image> images { get; set; }
    }
}
