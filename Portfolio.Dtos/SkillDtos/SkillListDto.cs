using Portfolio.Dtos.Interfaces;

namespace Portfolio.Dtos
{
    public class SkillListDto : IDto
    {
        public int id { get; set; }
        public string skillName { get; set; }
        public short skillPercent { get; set; }
        public short rank { get; set; }
    }
}
