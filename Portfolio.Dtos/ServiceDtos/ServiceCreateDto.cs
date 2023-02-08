using Portfolio.Dtos.Interfaces;

namespace Portfolio.Dtos
{
    public class ServiceCreateDto : IDto
    {
        public string serviceName { get; set; }
        public string iconPath { get; set; }
        public short rank { get; set; }
        public bool isActive { get; set; }
    }
}
