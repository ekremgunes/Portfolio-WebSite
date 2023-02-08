namespace Portfolio.Entities
{
    public class Image : BaseEntity
    {
        public string imgPath { get; set; }
        public int contentDetailId { get; set; }
        public ContentDetail contentDetail { get; set; }

    }
}
