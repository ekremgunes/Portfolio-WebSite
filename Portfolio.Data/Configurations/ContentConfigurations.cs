using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Entities;

namespace Portfolio.DataAccess.Configurations
{
    public class ContentConfigurations : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.contentName).HasMaxLength(150).IsRequired();
            builder.Property(x => x.imagePath).IsRequired();
            builder.Property(x => x.rank).HasColumnType("smallint").HasDefaultValueSql("99");
            builder.HasMany(x => x.Details).WithOne(x => x.content).HasForeignKey(x => x.contentId);
            builder.Property(x => x.InShowCase).HasDefaultValueSql("0");
        }
    }
}
