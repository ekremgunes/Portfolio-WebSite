namespace Portfolio.Entities
{
    public class Skill : BaseEntity
    {
        public string skillName { get; set; }
        public short skillPercent { get; set; }
        public short rank { get; set; }
    }
}
