using Portfolio.Dtos.Interfaces;

namespace Portfolio.Dtos
{
    public class ContentListDto : IDto
    {
        public int id { get; set; }
        public string contentName { get; set; }
        public string imagePath { get; set; }
        public short rank { get; set; }
        public bool InShowCase { get; set; }
        public List<ContentDetailListDto> Details { get; set; }
    }
}
