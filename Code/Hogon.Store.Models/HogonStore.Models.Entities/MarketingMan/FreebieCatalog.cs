using Hogon.Framework.Core.UnitOfWork.EntityFramework;

namespace Hogon.Store.Models.Entities.MarketingMan
{
    /// <summary>
    /// 赠品分类
    /// </summary>
    public class FreebieCatalog : BaseEntity
    {
        /// <summary>
        /// 赠品分类名称
        /// </summary>
        public string FreebieCatalogName { get; set; }

        /// <summary>
        /// 赠品分类排序
        /// </summary>
        public int Sort { set; get; }
    }
}
