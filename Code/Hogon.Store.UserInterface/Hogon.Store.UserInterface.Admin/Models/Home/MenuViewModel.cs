using Hogon.Framework.Utilities.SmartList;
using Hogon.Framework.Utilities.SmartList.Attributes;
using System;

namespace Hogon.Store.UserInterface.Admin.Models.Home
{
    public class MenuViewModel
    {
        /// <summary>
        /// 菜单Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 父菜单Id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 上级名称
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// 成员名称
        /// </summary>
        [Field("成员名称")]
        [TextSearch("成员名称")]
        private string memberName { get; set; }

        /// <summary>
        /// 菜单编码
        /// </summary>
        [Field("菜单编码")]
        [TextSearch("菜单编码")]
        public string Code { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [LinkField("用户名称")]
        [TextSearch("菜单名称")]
        public string Name { get; set; }

        /// <summary>
        /// URL地址
        /// </summary>
        [Field("URL地址")]
        public string URL { get; set; }

        /// <summary>
        /// 小图标
        /// </summary>
        [Field("小图标")]
        public string Icon { get; set; }

        /// <summary>
        /// 菜单是否可用
        /// </summary>
        [Field("是否可用")]
        public string DisplayIsEnable
        {
            get
            {
                return IsEnable ? "可用" : "不可用";
            }
        }

        /// <summary>
        /// 菜单是否可用
        /// </summary>
        public bool IsEnable { get; set; }

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
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DateSearch("创建时间", ComparisonType.MoreThan)]
        [DateSearch("至", ComparisonType.LessThan)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 菜单修改时间
        /// </summary>
        public DateTime UpdatedTime { get; set; }

        /// <summary>
        /// 菜单创建人Id
        /// </summary>
        public int CreatorId { get; set; }

        /// <summary>
        /// 菜单修改人Id
        /// </summary>
        public int UpdaterId { get; set; }
    }
}