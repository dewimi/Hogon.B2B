using Hogon.Store.Models.Entities.GoodsMan;
using System.Collections.Generic;

namespace Hogon.Store.Models.Entities.MarketingMan
{
    /// <summary>
    /// 组合套餐
    /// </summary>
    public class PackageGoods : Goods
    {
        public PackageGoods()
        {
            Rel_PackageGoods_ProductGoodses = new HashSet<Rel_PackageGoods_ProductGoods>();
        }
        /// <summary>
		/// 子商品集合
		/// </summary>
        public override ICollection<Goods> ChildrenGoods { get; set; }
       

        /// <summary>
        /// 组合商品 产品商品关联
        /// </summary>
         public  ICollection<Rel_PackageGoods_ProductGoods> Rel_PackageGoods_ProductGoodses { get; set; }
    }
}
