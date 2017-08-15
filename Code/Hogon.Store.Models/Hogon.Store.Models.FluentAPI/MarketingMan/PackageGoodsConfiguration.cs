using Hogon.Store.Models.Entities.MarketingMan;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.FluentAPI.MarketingMan
{
    class PackageGoodsConfiguration : EntityTypeConfiguration<PackageGoods>
    {
        public PackageGoodsConfiguration()
        {
            HasOptional(m => m.ProductGoods).WithMany().Map(m => m.MapKey("ProductGoodsId"));
        }
    }
}
