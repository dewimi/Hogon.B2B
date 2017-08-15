using Hogon.Store.Models.Entities.GoodsMan;
using System.Data.Entity.ModelConfiguration;

namespace Hogon.Store.Models.FluentAPI.GoodsMan
{
    public class GoodsTypeConfiguration : EntityTypeConfiguration<GoodsType>
    {

        public GoodsTypeConfiguration()
        {
            Property(p => p.Name).HasMaxLength(20);
            Property(p => p.Order).IsRequired();
            HasMany(p => p.Children).WithOptional(g=>g.Parent).HasForeignKey(p => p.ParentId);
            
            HasRequired(m => m.ProductType).WithMany(m =>m.GoodsClass).Map(m=>m.MapKey("ProductTypeId"));
        }
    }
}
