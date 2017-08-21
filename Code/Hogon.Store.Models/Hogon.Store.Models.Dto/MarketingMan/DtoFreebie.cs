using Hogon.Store.Models.Dto.GoodsMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.Dto.MarketingMan
{
    public class DtoFreebie:BaseDto
    {
        /// <summary>
        /// 赠品分类
        /// </summary>
        //public DtoFreebieCatalog dtoFreebieCatalog { get; set; }

        /// <summary>
        /// 赠品分类名称
        /// </summary>
        public string FreebieCatalogName { get; set; }

        /// <summary>
        /// 赠品分类排序
        /// </summary>
        public int Sort { set; get; }

        /// <summary>
        /// 赠品分类Id
        /// </summary>
        public Guid FreebieCatalogId { get; set; }

        /// <summary>
        /// 赠品明细集合
        /// </summary>
        public ICollection<DtoFreebieLine> FreebieLines { get; set; }

        ///// <summary>
        ///// 产品
        ///// </summary>
        //public DtoPro Product { get; set; }
        public string ProductName { get; set; }

        public string ProductCode { get; set; }

        public Guid ProductId { get; set; }

        /// <summary>
        /// 是否发布
        /// </summary>
        public bool IsPublish { get; set; }

        /// <summary>
        /// 赠品排序序号
        /// </summary>
        public int FreebiSortNum { get; set; }

        /// <summary>
        /// 每单限购数量
        /// </summary>
        public int LimitBuyAmount { get; set; }

        /// <summary>
        /// 赠品描述
        /// </summary>
        public string Description { get; set; }

    }
}
