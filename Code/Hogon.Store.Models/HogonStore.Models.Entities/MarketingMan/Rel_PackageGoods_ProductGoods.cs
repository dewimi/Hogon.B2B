using Hogon.Framework.Core.UnitOfWork.EntityFramework;
using Hogon.Store.Models.Entities.GoodsMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.Entities.MarketingMan
{
    public class Rel_PackageGoods_ProductGoods : BaseEntity
    {
        /// <summary>
        /// 组合套餐
        /// </summary>
        public PackageGoods PackageGoods { get; set; }

        /// <summary>
        /// 产品商品
        /// </summary>
        public ProductGoods ProductGoods { get; set; }
    }
}
