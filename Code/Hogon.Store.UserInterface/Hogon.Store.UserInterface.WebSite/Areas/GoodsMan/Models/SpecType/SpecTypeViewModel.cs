using Hogon.Framework.Utilities.SmartList.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hogon.Store.UserInterface.WebSite.Areas.GoodsMan.Models.SpecType
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
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }

        [Field("创建人")]
        /// <summary>
        /// 创建人
        /// </summary>
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
        [Field("创建时间")]
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
    }
}