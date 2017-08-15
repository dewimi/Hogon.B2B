using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.Entities.GoodsMan
{
    /// <summary>
    /// 品牌商品分类关联
    /// </summary>
    public class Rela_Brand_GoodsType
    {

        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public Brand Brand { get; set; }

        /// <summary>
        /// 商品分类
        /// </summary>
        public GoodsType GoodsType { get; set; }
    }
}
