using Hogon.Framework.Core.UnitOfWork.EntityFramework;
using Hogon.Store.Models.Entities.GoodsMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.Entities.MarketingMan
{

    /// <summary>
    /// 赠品
    /// </summary>
    public class Freebie: BaseEntity
    {
        /// <summary>
        /// 赠品分类
        /// </summary>
        public FreebieCatalog FreebieCatalog { get; set; }

        /// <summary>
        /// 赠品明细集合
        /// </summary>
        public ICollection<FreebieLine> FreebieLines { get; set; }

        /// <summary>
        /// 产品
        /// </summary>
        public Product Product { get; set; }

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
        /// 描述
        /// </summary>
        public string Description { get; set; }


    }
}
