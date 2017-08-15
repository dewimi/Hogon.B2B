using System.Data.Entity.ModelConfiguration;
using Hogon.Store.Models.Entities.Security;
using Hogon.Framework.Core.UnitOfWork.EntityFramework;

namespace Hogon.Store.Models.FluentAPI.Security
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(e => e.Name).HasMaxLength(20).IsRequired().HasUniqueIndexAnnotation("UserNameIndexer", 1);
            Property(e => e.NickName).HasMaxLength(20);
            Property(e => e.Password).HasMaxLength(50);
            Property(e => e.Email).HasMaxLength(100);            
        }
    }
}
