using Portfolio.Dtos.Interfaces;

namespace Portfolio.Dtos
{
    public class ContentCreateDto : IDto
    {
        public string contentName { get; set; }
        public string imagePath { get; set; }
        public short rank { get; set; }
        public bool InShowCase { get; set; }
    }
}
