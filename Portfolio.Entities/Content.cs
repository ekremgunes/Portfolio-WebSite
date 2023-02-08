namespace Portfolio.Entities
{
    public class Content : BaseEntity
    {
        public string contentName { get; set; }
        public string imagePath { get; set; }
        public short rank { get; set; }
        public bool? InShowCase { get; set; }
        public List<ContentDetail> Details { get; set; }
    }
}
