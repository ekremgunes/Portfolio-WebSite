using Microsoft.EntityFrameworkCore;
using Portfolio.DataAccess.Configurations;
using Portfolio.Entities;

namespace Portfolio.DataAccess.Context
{
    public class PortfolioContext : DbContext
    {
        public PortfolioContext(DbContextOptions<PortfolioContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MessageConfigurations());
            modelBuilder.ApplyConfiguration(new ContentConfigurations());
            modelBuilder.ApplyConfiguration(new ContentDetailConfigurations());
            modelBuilder.ApplyConfiguration(new SkillConfigurations());
            modelBuilder.ApplyConfiguration(new SettingsConfigurations());
            modelBuilder.ApplyConfiguration(new ImageConfigurations());
            modelBuilder.ApplyConfiguration(new ServiceConfigurations());
        }
        public DbSet<Content> Contents { get; set; }
        public DbSet<ContentDetail> ContentDetails { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Message> Messages { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.LogTo(Console.WriteLine);
        //}

    }
}
