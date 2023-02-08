using Portfolio.Dtos.Interfaces;

namespace Portfolio.Dtos
{
    public class ImageListDto : IDto
    {
        public int id { get; set; }
        public string imgPath { get; set; }
        public int contentDetailId { get; set; }
        public ContentDetailListDto contentDetail { get; set; }

    }
}
