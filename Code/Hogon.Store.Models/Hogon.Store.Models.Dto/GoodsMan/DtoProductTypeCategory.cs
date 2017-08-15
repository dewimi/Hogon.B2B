using System;

namespace Hogon.Store.Models.Dto.GoodsMan
{
    /// <summary>
    /// 产品类型分类
    /// </summary>
    public class DtoProductCategory : BaseDto
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string ProductTypeCategoryName { get; set; }

        /// <summary>
        /// 所属分类Id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 父级分类名称
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// 分类编码
        /// </summary>
        public string Code { get; set; }
    }
}
