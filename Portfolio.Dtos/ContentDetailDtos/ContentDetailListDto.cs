using Portfolio.Dtos.Interfaces;

namespace Portfolio.Dtos
{
    public class ContentDetailListDto : IDto
    {
        public int id { get; set; }
        public string contentDetailName { get; set; }
        public string imagePath { get; set; }
        public short rank { get; set; }

        public int contentId { get; set; }
        public ContentListDto content { get; set; }
        public List<ImageListDto> images { get; set; }
    }
}
