using Hogon.Framework.Core.UnitOfWork.EntityFramework;
using Hogon.Store.Models.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.Entities.MemberMan
{
    /// <summary>
    /// 账号
    /// </summary>
    public abstract class Account:BaseEntity
    {
        public Account()
        {
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 联系人邮箱
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// 获取可用的菜单集合
        /// </summary>
        /// <returns></returns>
        public abstract IQueryable<Menu> GetAvailableMenus();

        /// <summary>
        /// 获取可用的功能集合
        /// </summary>
        /// <returns></returns>
        public abstract IQueryable<Function> GetAvailableFunctions();
    }
}
