using Hogon.Store.Models.Entities.MemberMan;
using System;
using System.Collections.Generic;

namespace Hogon.Store.Models.Entities.Security
{
    /// <summary>
    /// 角色接口
    /// </summary>
    public interface IRole
    {
        Guid Id { get; }

        string CreatePerson { get; }

        DateTime CreateTime { get; }

        string UpdatePerson { get; }

        DateTime UpdateTime { get; }

        /// <summary>
        /// 角色名称
        /// </summary>
        string RoleName { get; }

        /// <summary>
        /// 是否可用
        /// </summary>
        bool IsEnable { get; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        bool IsAdministrator { get; }

        /// <summary>
        /// 角色从属主体
        /// </summary>
        Account PrimaryAccount { get; }

        /// <summary>
        /// 获取已授权的菜单集合
        /// </summary>
        IEnumerable<Menu> GetAuthroizedMenus();

        /// <summary>
        /// 获取角色下所有已授权的功能集合
        /// </summary>
        IEnumerable<Function> GetAuthorizedFunctions();

        /// <summary>
        /// 根据菜单Id获取已授权的功能集合
        /// </summary>
        IEnumerable<Function> GetAuthorizedFunctions(Guid menuId);
    }
}
