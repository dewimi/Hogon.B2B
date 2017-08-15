using Hogon.Framework.Utilities.SmartList.Attributes;
using Hogon.Store.Models.Entities.GoodsMan;
using System;

namespace Hogon.Store.UserInterface.Admin.Areas.GoodsMan.Models
{
    public class GoodsTypeViewModel
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        [Field("类型名称")]
        public string TypeName { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        public ProductType ProductType { get; set; }

        /// <summary>
        /// 上级分类
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 父级分类名称
        /// </summary>
        [Field("上级类型名称")]
        public string ParentName { get; set; }

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