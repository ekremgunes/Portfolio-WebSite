using Portfolio.Dtos.Interfaces;

namespace Portfolio.Dtos
{
    public class ImageCreateDto : IDto
    {
        public string imgPath { get; set; }
        public int contentDetailId { get; set; }
    }
}
