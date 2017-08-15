using Hogon.Store.Models.Entities.GoodsMan;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.FluentAPI.GoodsMan
{
    public class Rela_Brand_GoodsTypeConfiguration : EntityTypeConfiguration<Rela_Brand_GoodsType>
    {
        public Rela_Brand_GoodsTypeConfiguration()
        {
            HasRequired(m => m.Brand).WithMany(m => m.Rela_Brand_GoodsType).Map(m => m.MapKey("BrandId"));
            HasRequired(m => m.GoodsType).WithMany(m => m.Rela_Brand_GoodsType).Map(m => m.MapKey("GoodsTypeId"));
        }
    }
}
