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

        /// <summary>
        /// 是否包邮
        /// </summary>
        public bool IsFreePost { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }

        /// <summary>
        /// 套餐现价
        /// </summary>
        public decimal CurrentPrice { get; set; }

    }
}
