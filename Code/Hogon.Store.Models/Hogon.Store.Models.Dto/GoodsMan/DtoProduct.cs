using System;
using System.Collections.Generic;

namespace Hogon.Store.Models.Dto.GoodsMan
{
    /// <summary>
    /// 产品管理
    /// </summary>
    public class DtoProduct : BaseDto
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 产品编码
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 产品型号
        /// </summary>
        public DtoProductType ProductType { get; set; }

        /// <summary>
        /// 产品类型名称
        /// </summary>
        public string ProductTypeName { get; set; }

        /// <summary>
        /// 产品型号Id
        /// </summary>
        public Guid ProductTypeId { get; set; }

        /// <summary>
        /// 品牌Id
        /// </summary>
        public Guid BrandId { get; set; }

        /// <summary>
        /// 品牌名称
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public DtoBrand Brand { get; set; }

        /// <summary>
        /// 搜索关键词
        /// </summary>
        public string SearchPrimaryKey { get; set; }

        /// <summary>
        /// 产品描述
        /// </summary>
        public string ProductDescription { get; set; }

        /// <summary>
        /// 销售基准价
        /// </summary>
        public decimal SalerBasicPrice { get; set; }

        /// <summary>
        /// 最低销售价
        /// </summary>
        public decimal SalerMinPrice { get; set; }

        /// <summary>
        /// 促销是否影响最低价
        /// </summary>
        public bool IsEffectMinPrice { get; set; }

        /// <summary>
        /// 是否开启最低价预警
        /// </summary>
        public bool IsEnableMinPriceWarning { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
        public ICollection<DtoProductGoods> ProductGoods { get; set; }

        /// <summary>
        /// 规格类型String
        /// </summary>
        public string DiplaySpecType { get; set; }

        /// <summary>
        /// 规格参数模板String
        /// </summary>
        public string SpecParameterTemplate { get; set; }

        /// <summary>
        /// 服务商品集合
        /// </summary>
        public ICollection<DtoServiceGoods> ServiceGoods { get; set; }
    }
}
