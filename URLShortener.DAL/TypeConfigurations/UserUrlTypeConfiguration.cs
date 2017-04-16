using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using URLShortener.Domain.Database.Entities;

namespace URLShortener.DAL.TypeConfigurations
{
    public class UserUrlTypeConfiguration : EntityTypeConfiguration<UserUrl>
    {
        public UserUrlTypeConfiguration()
        {
            HasKey(p => p.Id).Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.CompactUrl).HasMaxLength(128);
            Property(p => p.OriginUrl).HasMaxLength(2000);
        }
    }
}
