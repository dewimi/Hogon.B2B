using Hogon.Framework.Utilities.SmartList.Attributes;
using System;

namespace Hogon.Store.UserInterface.Admin.Areas.GoodsMan.Models
{
    public class ProductTypeCategoryViewModel
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
        public string ProductTypeCategoryName { get; set; }

        /// <summary>
        /// 所属分类
        /// </summary>
     
        public Guid? ParentId { get; set; }

        [Field("上级分类")]
        [TextSearch("上级分类")]
        public string  ParentName { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Field("分类编码")]
        [TextSearch("分类编码")]
        public string Code { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set;  }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatePerson { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        public string DisplayCreateTime
        {
            get
            {
                return CreateTime.ToShortDateString();
            }
            set
            {
            }
        }

        /// <summary>
        /// 更新人
        /// </summary>
        [Field("修改人")]
        public string UpdatePerson { get; set; }

        [Field("修改时间")]
        public string DisplayUpdateTime
        {
            get
            {
                return UpdateTime.ToShortDateString();
            }
            set
            {
            }
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime {get; set;}
     
    }
}