using Hogon.Framework.Core.UnitOfWork.EntityFramework;
using Hogon.Store.Models.Entities.Common;
using Hogon.Store.Models.Entities.MarketingMan;
using System.Collections.Generic;

namespace Hogon.Store.Models.Entities.GoodsMan
{
    /// <summary>
    /// 商品信息
    /// </summary>
    public abstract class Goods :BaseEntity
    {
        public Goods()
        {
            Rela_Goods_GoodsType = new HashSet<Rela_Goods_GoodsType>();
            Rel_Promotion_Goods = new HashSet<Rel_Promotion_Goods>();
            ChildrenGoods = new HashSet<Goods>();
        }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        public string GoodsCode { get; set; }

        /// <summary>
        /// 商品描述
        /// </summary>
        public string GoodsDesription { get; set; }

        /// <summary>
        /// 图片文件
        /// </summary>
        public FileUpload FileUpload { get; set; }

        /// <summary>
        /// 商品别名
        /// </summary>
        public string GoodsAlias { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SalePrice { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// 子商品集合
        /// </summary>
        public abstract ICollection<Goods> ChildrenGoods { get; set; }

        /// <summary>
        /// 商品商品分类关联集合
        /// </summary>
        public virtual ICollection<Rela_Goods_GoodsType> Rela_Goods_GoodsType { get; set; }

        /// <summary>
        /// 促销-商品关联集合
        /// </summary>
        public virtual ICollection<Rel_Promotion_Goods> Rel_Promotion_Goods { get; set; }
    }
}
