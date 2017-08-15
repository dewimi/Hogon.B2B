using Hogon.Framework.Utilities.SmartList;
using Hogon.Framework.Utilities.SmartList.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hogon.Store.UserInterface.Admin.Areas.Security.Models.User
{
    public class UserViewModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        [LinkField("用户名称")]
        [TextSearch("用户名称")]
        public string Name { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [Field("昵称")]
        [TextSearch("昵称")]
        public string NickName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [Field("邮箱")]
        public string Email { get; set; }
        /// <summary>
        /// 用户是否可用
        /// </summary>
        [Field("是否可用")]
        public string DisplayIsEnable
        {
            get
            {
                return IsEnable ? "可用" : "不可用";
            }
            set
            {

            }
        }
        /// <summary>
        /// 是否可用
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
                return CreatedTime.ToShortDateString();
            }
            set
            {
            }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DateSearch("创建时间", ComparisonType.MoreThan)]
        [DateSearch("至", ComparisonType.LessThan)]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public string DisplayUpdatedTime
        {
            get
            {
                return UpdatedTime.ToShortDateString();
            }
            set
            {
            }
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdatedTime { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public int CreatorId { get; set; }

        /// <summary>
        /// 修改人Id
        /// </summary>
        public int UpdaterId { get; set; }
    }
}