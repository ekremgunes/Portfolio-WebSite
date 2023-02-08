using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Entities;

namespace Portfolio.DataAccess.Configurations
{
    public class SkillConfigurations : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.rank).HasColumnType("smallint").HasDefaultValueSql("99");
            builder.Property(x => x.skillName).IsRequired();
            builder.Property(x => x.skillPercent).HasColumnType("smallint").HasDefaultValue("20").IsRequired();

        }
    }
}
