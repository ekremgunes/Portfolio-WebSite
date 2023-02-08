using Portfolio.Dtos.Interfaces;

namespace Portfolio.Dtos
{
    public class ContentDetailUpdateDto : IUpdateDto
    {
        public int id { get; set; }
        public string contentDetailName { get; set; }
        public string imagePath { get; set; }
        public short rank { get; set; }

        public int contentId { get; set; }

    }
}
