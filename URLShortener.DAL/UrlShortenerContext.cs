using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using URLShortener.DAL.TypeConfigurations;
using URLShortener.Domain.Database.Entities;

namespace URLShortener.DAL
{
    public class UrlShortenerContext : DbContext
    {
        public UrlShortenerContext() : base("UrlShortenerContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserUrl> UserUrls { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            #region Configurations
            modelBuilder.Configurations.Add(new UserTypeConfiguration());
            modelBuilder.Configurations.Add(new UserUrlTypeConfiguration());
            #endregion

            modelBuilder.Entity<UserUrl>()
                .HasRequired(p => p.User)
                .WithMany(p => p.UserUrls)
                .HasForeignKey(p => p.UserId);
        }
    }
}
