using Hogon.Store.Models.Entities.MarketingMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.Entities.GoodsMan
{
    /// <summary>
    /// 产品商品
    /// </summary>
    public class ProductGoods : Goods
    {
        public ProductGoods()
        {
            FreebieLines = new HashSet<FreebieLine>();
            SpecParameterTemplate = new HashSet<SpecParameterTemplate>();
            Rel_PackageGoods_ProductGoodses = new HashSet<Rel_PackageGoods_ProductGoods>();
        }

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
        /// 产品信息
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// 规格参数模板
        /// </summary>
        public ICollection<SpecParameterTemplate> SpecParameterTemplate { get; set; }

        /// <summary>
        /// 赠品明细集合
        /// </summary>
        public ICollection<FreebieLine> FreebieLines { get; set; }

        /// <summary>
        /// 配件
        /// </summary>
        public override ICollection<Goods> ChildrenGoods { get; set; }

        /// <summary>
        /// 显示规格参数，格式：（规格类型：规格参数）
        /// </summary>
        public string DisplaySpecParameterTemplateName { get; set; }

        /// <summary>
        /// 组合商品 产品商品关联
        /// </summary>
        public ICollection<Rel_PackageGoods_ProductGoods> Rel_PackageGoods_ProductGoodses { get; set; }

    }
}
