using Hogon.Store.Models.Entities.GoodsMan;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.FluentAPI.GoodsMan
{
    public class ServiceGoodsConfiguration : EntityTypeConfiguration<ServiceGoods>
    {

        public ServiceGoodsConfiguration()
        {
            Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("ServiceGoods");
            });

            HasOptional(m => m.Product).WithMany(m => m.ServiceGoods).Map(m => m.MapKey("ServiceGoods_ProductId"));
        }
    }
}
