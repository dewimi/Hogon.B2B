using Hogon.Framework.Core.UnitOfWork.EntityFramework;
using Hogon.Store.Models.Entities.GoodsMan;
using System.Data.Entity.ModelConfiguration;

namespace Hogon.Store.Models.FluentAPI.GoodsMan
{
    public class GoodsConfiguration : EntityTypeConfiguration<Goods>
    {
        public GoodsConfiguration()
        {
            Property(p => p.GoodsName).HasMaxLength(20);
            Property(p => p.GoodsCode).HasMaxLength(50).IsRequired().HasUniqueIndexAnnotation("GoodsCodeIndexer", 1);
			Property(p => p.GoodsDesription).HasMaxLength(300);
            Property(p => p.GoodsAlias).HasMaxLength(20);
            Property(p => p.SalePrice).IsRequired();

		}
    }
}
