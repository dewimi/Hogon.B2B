using Hogon.Framework.Core.UnitOfWork.EntityFramework;
using Hogon.Store.Models.Entities.GoodsMan;

namespace Hogon.Store.Models.Entities.MarketingMan
{
    /// <summary>
    /// 赠品明细
    /// </summary>
    public class FreebieLine : BaseEntity
    {

       /// <summary>
       /// 配额
       /// </summary>
       public int Quota { get; set; }

        /// <summary>
        /// 赠品
        /// </summary>
        public Freebie Freebie { get; set; }

        /// <summary>
        /// 产品商品
        /// </summary>
        public ProductGoods ProductGoods { get; set; }
    }
}
