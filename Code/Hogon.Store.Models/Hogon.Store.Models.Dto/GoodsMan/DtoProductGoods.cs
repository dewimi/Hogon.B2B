using Hogon.Store.Models.Entities.GoodsMan;
using System;
using System.Collections.Generic;

namespace Hogon.Store.Models.Dto.GoodsMan
{
    /// <summary>
    /// 商品管理
    /// </summary>
    public class DtoProductGoods : BaseDto
    {
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
        /// 是否上架
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// 搜索关键词
        /// </summary>
        public string SearchKeywords { get; set; }

        /// <summary>
        /// 是否为默认商品
        /// </summary>
        public bool IsDefaultGoods { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// 规格参数名称
        /// </summary>
        public string SpecParameterS { get; set;}

        /// <summary>
        /// 规格参数模板
        /// </summary>
        public ICollection<DtoSpecParameterTemplate> DtoSpecParameterTemplateS { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 产品Id
        /// </summary>
        public Guid ProductId{ get; set; }

        /// <summary>
        /// 产品信息
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// 配件
        /// </summary>
       // public ICollection<Goods> ChildrenGoods { get; set; }

        /// <summary>
        /// 显示规格参数，格式：（规格类型：规格参数）
        /// </summary>
        public string DisplaySpecParameterTemplateName { get; set; }
    }
}
