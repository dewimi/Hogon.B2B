using System;

namespace Hogon.Store.Models.Dto.GoodsMan
{
    /// <summary>
    /// 商品分类
    /// </summary>
    public class DtoGoodsType:BaseDto
    {
        /// <summary>
        /// 产品类型Id
        /// </summary>
        public Guid ProductTypeId { get; set; }

		/// <summary>
		/// 产品类型名称
		/// </summary>
		public string ProductTypeName { get; set; }

		/// <summary>
		///商品上级分类名称
		/// </summary>
		public string ParentName { get; set; }

		/// <summary>
		/// 分类名称
		/// </summary>
		public string Name { get; set; }

        /// <summary>
        /// 分类描述
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// 上级分类
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}
