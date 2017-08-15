using Hogon.Framework.Utilities.SmartList.Attributes;
using System;

namespace Hogon.Store.UserInterface.Admin.Areas.GoodsMan.Models
{
    public class ProductViewModel
    {
        /// <summary>
        /// 产品Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [Field("商品名称")]
        [TextSearch("商品名称")]
        public string ProductName { get; set; }

        /// <summary>
        /// 产品编码
        /// </summary>
        [Field("商品编码")]
        [TextSearch("商品编码")]
        public string ProductCode { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        [Field("商品类型")]
        [TextSearch("商品类型")]
        public string ProductTypeName { get; set; }

        /// <summary>
        /// 搜索关键词
        /// </summary>
        [Field("搜索关键词")]
        public string SearchPrimaryKey { get; set; }

        /// <summary>
        /// 产品描述
        /// </summary>
        [Field("产品描述")]
        public string ProductDescription { get; set; }

        /// <summary>
        /// 销售基准价
        /// </summary>
        [Field("销售基准价")]
        public decimal SalerBasicPrice { get; set; }

        /// <summary>
        /// 最低销售价
        /// </summary>
        [Field("最低销售价")]
        public decimal SalerMinPrice { get; set; }
    }
}
