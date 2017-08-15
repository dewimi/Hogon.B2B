using Hogon.Framework.Utilities.SmartList.Attributes;
using Hogon.Store.Models.Entities.GoodsMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hogon.Store.UserInterface.Admin.Areas.GoodsMan.Models
{
    /// <summary>
    /// 产品商品
    /// </summary>
    public class ProductGoodsViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [Field("商品名称")]
        [TextSearch("商品名称")]
        public string GoodsName { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        [Field("商品编码")]
        [TextSearch("商品编码")]
        public string GoodsCode { get; set; }

        /// <summary>
        /// 商品描述
        /// </summary>
        public string GoodsDesription { get; set; }

        /// <summary>
        /// 商品别名
        /// </summary>
        [Field("商品别名")]
        public string GoodsAlias { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        [Field("销售价")]
        public decimal SalePrice { get; set; }

        /// <summary>
        /// 是否上架
        /// </summary>
        public decimal IsAvailable { get; set; }

        /// <summary>
        /// 搜索关键词
        /// </summary>
        [Field("搜索关键词")]
        [TextSearch("搜索关键词")]
        public string SearchKeywords { get; set; }

        /// <summary>
        /// 是否为默认商品
        /// </summary>
        public bool IsDefaultGoods { get; set; }

        /// <summary>
        /// 规格参数名称
        /// </summary>
        [Field("规格参数")]
        public string SpecParameterS { get; set; }

        /// <summary>
        /// 规格参数模板
        /// </summary>
        public IEnumerable<SpecParameterTemplate> SpecParameterTemplate { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [Field("产品名称")]
        public string ProductName { get; set; }

        /// <summary>
        /// 产品信息
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// 服务商品名称
        /// </summary>
        [Field("商品服务名称")]
        public string ServiccGoodsNames { get; set; }

        /// <summary>
        /// 服务商品集合
        /// </summary>
        public IEnumerable<ServiceGoods> ServiceGoods { get; set; }

        /// <summary>
        /// 子商品集合
        /// </summary>
        public IEnumerable<Goods> ChildrenGoods { get; set; }
    }
}