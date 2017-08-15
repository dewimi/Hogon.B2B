using Hogon.Store.Models.Entities.GoodsMan;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.FluentAPI.GoodsMan
{
    public class Rela_Goods_GoodsTypeConfiguration : EntityTypeConfiguration<Rela_Goods_GoodsType>
    {

        public Rela_Goods_GoodsTypeConfiguration()
        {
            HasRequired(m => m.Goods).WithMany(m => m.Rela_Goods_GoodsType).Map(m => m.MapKey("GoodsId"));
            HasRequired(m => m.GoodsType).WithMany(m => m.Rela_Goods_GoodsType).Map(m => m.MapKey("GoodsTypeId"));
        }
    }
}
