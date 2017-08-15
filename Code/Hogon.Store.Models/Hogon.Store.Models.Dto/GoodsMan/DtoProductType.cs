using System;
using System.Collections.Generic;

namespace Hogon.Store.Models.Dto.GoodsMan
{
    /// <summary>
    /// 产品类型
    /// </summary>
    public class DtoProductType : BaseDto
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 产品分类名称
        /// </summary>
        public string ProductTypeCategoryName { get; set; }

        /// <summary>
        /// 产品分类名称
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// 规格类型
        /// </summary>
        public virtual ICollection<DtoSpecType> SpecType { get; set; }
    }
}
