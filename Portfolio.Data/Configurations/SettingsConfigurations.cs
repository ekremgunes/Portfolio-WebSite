using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Entities;

namespace Portfolio.DataAccess.Configurations
{
    public class SettingsConfigurations : IEntityTypeConfiguration<Settings>
    {
        public void Configure(EntityTypeBuilder<Settings> builder)
        {
            builder.HasKey(x => x.id);

            builder.Property(x => x.aboutContent).IsRequired();
            builder.Property(x => x.aboutHeader).IsRequired();
            builder.Property(x => x.aboutImgPath).IsRequired();

            builder.Property(x => x.sliderHeader).IsRequired();
            builder.Property(x => x.sliderServices).IsRequired();
            builder.Property(x => x.sliderImgPath).IsRequired();

            builder.Property(x => x.slogan).IsRequired();
            builder.Property(x => x.seo).IsRequired();
            builder.Property(x => x.email).IsRequired();
            builder.Property(x => x.logoImgPath).IsRequired();


            builder.Property(x => x.instagramUrl).IsRequired();
            builder.Property(x => x.twitterUrl).IsRequired();
            builder.Property(x => x.facebookUrl).IsRequired();

            //seed-data, we must have one settings object in DB
            builder.HasData(new Settings
            {
                id = 1,
                aboutContent = "about me text",
                aboutImgPath = "imgpathabout",
                aboutHeader = "aboutheader",
                sliderHeader = "slider header",
                sliderImgPath = "sliderimgpath",
                logoImgPath = "logo.png",
                sliderServices = "service area",
                slogan = "just do it",
                seo = "<meta description='Perfect website'>",
                facebookUrl = "facebook.com",
                instagramUrl = "instagram.com",
                twitterUrl = "twitter.com",
                email = "info@portfolio.com",
                facebookVisibility = true,
                instagramVisibility = true,
                twitterVisibility = true,
                timedMessage = true

            });
        }
    }
}
