using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Entities;

namespace Portfolio.DataAccess.Configurations
{
    public class MessageConfigurations : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.senderMail).IsRequired();
            builder.Property(x => x.senderName).IsRequired();
            builder.Property(x => x.messageContent).HasMaxLength(350).IsRequired();
            builder.Property(x => x.date).HasDefaultValueSql("getdate()");
            builder.Property(x => x.isRead).HasDefaultValueSql("0");



        }
    }
}
