using Hogon.Store.Models.Entities.Security;
using System.Data.Entity.ModelConfiguration;

namespace Hogon.Store.Models.FluentAPI.Security
{
    public class Rela_Authority_FunctionConfiguration : EntityTypeConfiguration<Rela_Authority_Function>
    {
        public Rela_Authority_FunctionConfiguration()
        {
            HasRequired(m => m.Authority).WithMany(m => m.Rela_Authority_Function)
                .Map(m => m.MapKey("AuthorityId")).WillCascadeOnDelete(false);

            HasRequired(m => m.Function).WithMany(m => m.Rela_Authority_Function)
                .Map(m => m.MapKey("FunctionId"));
        }
    }
}
