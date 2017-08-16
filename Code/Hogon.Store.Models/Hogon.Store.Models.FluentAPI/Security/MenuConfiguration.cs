using Hogon.Framework.Core.UnitOfWork.EntityFramework;
using Hogon.Store.Models.Entities.Security;
using System.Data.Entity.ModelConfiguration;

namespace Hogon.Store.Models.FluentAPI.Security
{
    public class MenuConfiguration: EntityTypeConfiguration<Menu>
    {
        public MenuConfiguration()
        { 
            Property(e => e.Code).HasMaxLength(50).IsRequired().HasUniqueIndexAnnotation("MenuCodeIndexer", 1);
            Property(e => e.Name).HasMaxLength(20);
            Property(e => e.URL).HasMaxLength(60);

            HasMany(m => m.Children).WithOptional(m => m.Parent)
                .HasForeignKey(m => m.ParentId);
        }
    }
}
