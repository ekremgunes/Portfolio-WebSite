using Portfolio.Dtos.Interfaces;

namespace Portfolio.Dtos
{
    public class SkillUpdateDto : IUpdateDto
    {
        public int id { get; set; }
        public string skillName { get; set; }
        public short skillPercent { get; set; }
        public short rank { get; set; }
    }
}
