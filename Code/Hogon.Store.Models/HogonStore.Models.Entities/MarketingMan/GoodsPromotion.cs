using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.Entities.MarketingMan
{
    /// <summary>
    /// 商品促销
    /// </summary>
    public class GoodsPromotion : PromotinRule
    {
        public GoodsPromotion()
        {
            Rel_Promotion_Goods = new HashSet<Rel_Promotion_Goods>();
        }

        /// <summary>
        /// 促销-商品关联集合
        /// </summary>
        public virtual ICollection<Rel_Promotion_Goods> Rel_Promotion_Goods { get; set; }
    }
}
