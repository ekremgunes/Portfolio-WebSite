namespace Portfolio.Entities
{
    public class Service : BaseEntity
    {
        public string serviceName { get; set; }
        public string iconPath { get; set; }
        public short rank { get; set; }
        public bool? isActive { get; set; }
    }
}
