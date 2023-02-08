using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Entities;

namespace Portfolio.DataAccess.Configurations
{
    public class ContentDetailConfigurations : IEntityTypeConfiguration<ContentDetail>
    {
        public void Configure(EntityTypeBuilder<ContentDetail> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.rank).HasColumnType("smallint").HasDefaultValueSql("99");
            builder.Property(x => x.contentDetailName).HasMaxLength(250).IsRequired();
            builder.Property(x => x.contentId).IsRequired();
            builder.Property(x => x.imagePath).IsRequired();
        }
    }
}
