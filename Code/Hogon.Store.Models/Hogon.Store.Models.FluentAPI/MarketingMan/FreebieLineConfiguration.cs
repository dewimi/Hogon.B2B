using Hogon.Store.Models.Entities.MarketingMan;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.FluentAPI.MarketingMan
{
    public class FreebieLineConfiguration : EntityTypeConfiguration<FreebieLine>
    {
        public FreebieLineConfiguration()
        {

            HasRequired(m => m.ProductGoods).WithMany(m=>m.FreebieLines)
                .Map(m => m.MapKey("ProductGoodsId")).WillCascadeOnDelete(false);
            HasRequired(m => m.Freebie).WithMany(m => m.FreebieLines).Map(m => m.MapKey("FreebieId"));
        }
    }
}
