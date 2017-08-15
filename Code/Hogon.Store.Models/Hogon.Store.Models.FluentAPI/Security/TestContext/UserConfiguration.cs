using Hogon.Store.Models.Entities.TestContext;
using System.Data.Entity.ModelConfiguration;

namespace Hogon.Store.Models.FluentAPI.TestContext
{
    public class UserConfiguration : EntityTypeConfiguration<TestUser>
    {
        public UserConfiguration()
        {
            Property(e => e.Name).HasMaxLength(20);
        }
    }
}
