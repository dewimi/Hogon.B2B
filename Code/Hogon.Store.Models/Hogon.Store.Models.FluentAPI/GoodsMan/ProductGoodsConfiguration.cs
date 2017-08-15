using Hogon.Store.Models.Entities.GoodsMan;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.FluentAPI.GoodsMan
{
    public class ProductGoodsConfiguration: EntityTypeConfiguration<ProductGoods>
    {
        public ProductGoodsConfiguration()
        {
            HasRequired(m => m.Product).WithMany(m => m.ProductGoods).Map(m => m.MapKey("ProductId"));
        }
    }
}
