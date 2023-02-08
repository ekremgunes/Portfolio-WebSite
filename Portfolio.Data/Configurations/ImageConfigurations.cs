using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Entities;

namespace Portfolio.DataAccess.Configurations
{
    public class ImageConfigurations : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.imgPath).IsRequired();
            builder.HasOne(x => x.contentDetail).WithMany(x => x.images).HasForeignKey(x => x.contentDetailId);
        }
    }
}
