using Hogon.Store.Models.Entities.MarketingMan;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.FluentAPI.MarketingMan
{
    public class Rel_Promotion_GoodsConfiguration : EntityTypeConfiguration<Rel_Promotion_Goods>
    {
        public Rel_Promotion_GoodsConfiguration()
        {

            HasRequired(m => m.GoodsPromotion).WithMany(m => m.Rel_Promotion_Goods).Map(m => m.MapKey("PromotionId"));
            HasRequired(m => m.Goods).WithMany(m => m.Rel_Promotion_Goods).Map(m => m.MapKey("GoodsId"));
        }
    }
}
