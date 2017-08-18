using Hogon.Store.Models.Entities.MarketingMan;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.FluentAPI.MarketingMan
{
    public class PackageGoodsConfiguration : EntityTypeConfiguration<PackageGoods>
    {
        public PackageGoodsConfiguration()
        {
            Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("PackageGoods");
            });
        }
    }
}
