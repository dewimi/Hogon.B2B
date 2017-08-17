using Hogon.Store.Models.Entities.MemberMan;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hogon.Store.Models.Entities.Security
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role
    {
        public Role()
        {
            Rela_Role_Account = new HashSet<Rela_Role_Account>();
            Authorities = new HashSet<Authority>();
        }

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
        public bool IsEnable { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdministrator { get; set; }

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

        /// <summary>
        /// 企业
        /// </summary>
        public Enterprise Enterprise { get; set; }

        /// <summary>
        /// 用户角色关联集合
        /// </summary>
        public virtual ICollection<Rela_Role_Account> Rela_Role_Account { get; set; }

        /// <summary>
        /// 权限集合
        /// </summary>
        public virtual ICollection<Authority> Authorities { get; set; }

        /// <summary>
        /// 获取已授权的菜单集合
        /// </summary>
        public IEnumerable<Menu> GetAuthroizedMenus()
        {
            return Authorities.Select(m => m.Menu).Where(m=>m.IsEnable == true);
        }

        /// <summary>
        /// 获取角色下所有已授权的功能集合
        /// </summary>
        public IEnumerable<Function> GetAuthroizedFunctions()
        {
            var functions = Authorities.SelectMany(m => m.Rela_Authority_Function)
                .Select(m => m.Function).Where(m => m.IsEnable == true);

            return functions;
        }

        /// <summary>
        /// 根据菜单Id获取已授权的功能集合
        /// </summary>
        public IEnumerable<Function> GetAuthroizedFunctions(Guid menuId)
        {
            var functions = Authorities.SelectMany(m => m.Rela_Authority_Function)
                .Select(m => m.Function).Where(m=>m.Menu.Id == menuId);

            return functions;
        }
    }
}
