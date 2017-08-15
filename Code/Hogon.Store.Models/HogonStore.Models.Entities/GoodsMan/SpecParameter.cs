using Hogon.Framework.Core.UnitOfWork.EntityFramework;

namespace Hogon.Store.Models.Entities.GoodsMan
{
    /// <summary>
    /// 规格参数
    /// </summary>
    public class SpecTypeParameter: BaseEntity
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParameterName { get; set; }
    }
}
