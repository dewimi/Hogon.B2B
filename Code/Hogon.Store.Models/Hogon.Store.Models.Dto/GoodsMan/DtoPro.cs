using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.Dto.GoodsMan
{
    public class DtoPro:BaseDto
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        public DtoProductType ProductType { get; set; }

        /// <summary>
        /// 搜索关键词
        /// </summary>
        public string SearchPrimaryKey { get; set; }

        /// <summary>
        /// 产品描述
        /// </summary>
        public string ProductDescription { get; set; }

        /// <summary>
        /// 产品编码
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 销售基准价
        /// </summary>
        public decimal SalerBasicPrice { get; set; }

        /// <summary>
        /// 最低销售价
        /// </summary>
        public decimal SalerMinPrice { get; set; }

        /// <summary>
        /// 最低销售价类型
        /// </summary>
        //public SalerMinPriceType SalerMinPriceType { get; set; }

        /// <summary>
        /// 促销是否影响最低价
        /// </summary>
        public bool IsEffectMinPrice { get; set; }

        /// <summary>
        /// 是否开启最低价预警
        /// </summary>
        public bool IsEnableMinPriceWarning { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public DtoBrand Brand { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
        public ICollection<DtoProductGoods> ProductGoods { get; set; }

    }
}
