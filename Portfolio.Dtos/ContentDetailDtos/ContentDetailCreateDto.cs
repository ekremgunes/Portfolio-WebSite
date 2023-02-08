using Portfolio.Dtos.Interfaces;

namespace Portfolio.Dtos
{
    public class ContentDetailCreateDto : IDto
    {
        public string contentDetailName { get; set; }
        public string imagePath { get; set; }
        public short rank { get; set; }

        public int contentId { get; set; }

    }
}
