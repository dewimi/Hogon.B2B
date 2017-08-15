using Hogon.Store.Models.Entities.GoodsMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.Entities.MarketingMan
{
    /// <summary>
    /// 促销-商品关联
    /// </summary>
    public class Rel_Promotion_Goods
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 商品促销
        /// </summary>
       public GoodsPromotion GoodsPromotion { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
       public Goods Goods { get; set; }
    }
}
