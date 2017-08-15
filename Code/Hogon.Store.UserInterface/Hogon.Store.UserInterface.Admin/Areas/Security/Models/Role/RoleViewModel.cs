using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hogon.Store.UserInterface.Admin.Areas.Security.Models.Role
{
    public class RoleViewModel
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public Guid Id { get; set; }
      
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        public string IsEnable { get; set; }
        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdministrator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
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
        /// 角色创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 角色修改时间
        /// </summary>
        public DateTime UpdatedTime { get; set; }
        /// <summary>
        /// 角色创建人Id
        /// </summary>
        public Guid CreatorId { get; set; }
        /// <summary>
        /// 角色修改人Id
        /// </summary>
        public Guid UpdaterId { get; set; }
    }
}