using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Entities;

namespace Portfolio.DataAccess.Configurations
{
    public class ServiceConfigurations : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.serviceName).IsRequired();
            builder.Property(x => x.isActive).HasDefaultValueSql("1");
            builder.Property(x => x.rank).HasColumnType("smallint").HasDefaultValueSql("99");

        }
    }
}
