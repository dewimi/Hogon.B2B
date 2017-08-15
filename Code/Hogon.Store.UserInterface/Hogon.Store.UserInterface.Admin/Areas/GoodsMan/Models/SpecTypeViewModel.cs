using Hogon.Framework.Utilities.SmartList.Attributes;
using System;

namespace Hogon.Store.UserInterface.Admin.Areas.GoodsMan.Models
{
    public class SpecTypeViewModel
    {
        /// <summary>
        /// 规格Id
        /// </summary>
        public Guid Id { get; set; }

        [Field("规格名称")]
        [TextSearch("规格名称")]
        /// <summary>
        /// 规格名称
        /// </summary>
        public string SpecName { get; set; }

        [Field("商品类型")]
        /// <summary>
        /// 商品类型
        /// </summary>
        public string SpecCatalog { get; set; }

        [Field("规格备注")]
        /// <summary>
        /// 规格备注
        /// </summary>
        public string SpecRemark { get; set; }

        [Field("规格别名")]
        /// <summary>
        /// 规格别名
        /// </summary>
        public string SpecSecondName { get; set; }

        [Field("显示类型")]
        /// <summary>
        /// 显示类型
        /// </summary>
        public string DisplayName { get; set; }

        [Field("显示方式")]
        /// <summary>
        /// 显示方式
        /// </summary>
        public string DisplayMode { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Field("创建人")]
        public string CreatePerson { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Field("创建时间")]
        public string DisplayCreatedTime
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
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdatePerson { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
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
        ///产品类型Id 
        /// </summary>
        public Guid ProductTypeId { get; set; }
    }
}