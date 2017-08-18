using Hogon.Store.Models.Entities.MarketingMan;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.FluentAPI.MarketingMan
{
    public class Rel_PackageGoods_ProductGoodsConfiguration 
        : EntityTypeConfiguration<Rel_PackageGoods_ProductGoods>
    {
        public Rel_PackageGoods_ProductGoodsConfiguration()
        {

            HasRequired(m => m.PackageGoods).WithMany(m => m.Rel_PackageGoods_ProductGoodses).Map(m => m.MapKey("PackageGoodsId"));
            HasRequired(m => m.ProductGoods).WithMany(m => m.Rel_PackageGoods_ProductGoodses).Map(m => m.MapKey("ProductGoodsId"));
        }
    }
}
