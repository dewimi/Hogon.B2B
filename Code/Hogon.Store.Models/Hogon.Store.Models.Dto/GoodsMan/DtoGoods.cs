using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.Dto.GoodsMan
{
    public  abstract class DtoGoods:BaseDto
    {
        public DtoGoods()
        {
            DtoRela_Goods_GoodsType = new HashSet<DtoRela_Goods_GoodsType>();
            DtoRel_Promotion_Goods = new HashSet<DtoRel_Promotion_Goods>();
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
        public abstract ICollection<DtoGoods> ChildrenGoods { get; set; }

        /// <summary>
        /// 商品商品分类关联集合
        /// </summary>
        public virtual ICollection<DtoRela_Goods_GoodsType> DtoRela_Goods_GoodsType { get; set; }

        /// <summary>
        /// 促销-商品关联集合
        /// </summary>
        public virtual ICollection<DtoRel_Promotion_Goods> DtoRel_Promotion_Goods { get; set; }
    }
}
