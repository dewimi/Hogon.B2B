using Hogon.Framework.Utilities.SmartList.Attributes;
using System;

namespace Hogon.Store.UserInterface.Admin.Areas.GoodsMan.Models
{
    public class ProductTypeViewModel
    {
        /// <summary>
        /// 类型Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        [Field("类型名称")]
        [TextSearch("类型名称")]
        public string TypeName { get; set; }

        /// <summary>
        /// 所属分类
        /// </summary>
        [Field("所属分类")]
        public string ProductTypeCategoryName { get; set; }

        /// <summary>
        /// 产品分类名称
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatePerson { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建时间字符串
        /// </summary>
        [Field("创建时间")]
        public string DisplayCreateTime
        {
            get
            {
                return CreateTime.ToShortDateString();
            }
        }

        /// <summary>
        /// 更新人
        /// </summary>
        [Field("修改人")]
        public string UpdatePerson { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 修改时间字符串
        /// </summary>
        [Field("修改时间")]
        public string DisplayUpdateTime
        {
            get
            {
                return UpdateTime.ToShortDateString();
            }
        }
    }
}