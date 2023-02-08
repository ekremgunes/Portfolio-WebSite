using Portfolio.Dtos.Interfaces;

namespace Portfolio.Dtos
{
    public class ServiceUpdateDto : IUpdateDto
    {
        public int id { get; set; }
        public string serviceName { get; set; }
        public string iconPath { get; set; }
        public short rank { get; set; }
        public bool isActive { get; set; }
    }
}
