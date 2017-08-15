using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.Entities.GoodsMan
{
    /// <summary>
    /// 商品商品类型关联
    /// </summary>
    public class Rela_Goods_GoodsType
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
        public Goods Goods { get; set; }

        /// <summary>
        /// 商品类型
        /// </summary>
        public GoodsType GoodsType { get; set; }
    }
}
