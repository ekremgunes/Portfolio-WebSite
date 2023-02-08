using Portfolio.Dtos.Interfaces;

namespace Portfolio.Dtos
{
    public class SkillCreateDto : IDto
    {
        public string skillName { get; set; }
        public short skillPercent { get; set; }
        public short rank { get; set; }
    }
}
