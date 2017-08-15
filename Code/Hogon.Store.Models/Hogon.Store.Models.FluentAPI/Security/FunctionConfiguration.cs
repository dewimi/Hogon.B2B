using Hogon.Store.Models.Entities.Security;
using System.Data.Entity.ModelConfiguration;

namespace Hogon.Store.Models.FluentAPI.Security
{
    public class FunctionConfiguration : EntityTypeConfiguration<Function>
    {
        public FunctionConfiguration()
        {
            Property(e => e.Name).HasMaxLength(50);

            HasRequired(m => m.Menu).WithMany(m => m.Functions).Map(m => m.MapKey("MenuId"));
        }
    }
}
