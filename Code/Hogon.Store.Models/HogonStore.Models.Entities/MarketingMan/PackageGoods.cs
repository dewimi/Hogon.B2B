using Hogon.Store.Models.Entities.GoodsMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.Entities.MarketingMan
{
    /// <summary>
    /// 组合套餐
    /// </summary>
    public class PackageGoods : Goods
    {
        /// <summary>
		/// 子商品集合
		/// </summary>
        public override ICollection<Goods> ChildrenGoods { get; set; }
       

        /// <summary>
        /// 产品商品
        /// </summary>
        public ProductGoods ProductGoods { get; set; }
    }
}
