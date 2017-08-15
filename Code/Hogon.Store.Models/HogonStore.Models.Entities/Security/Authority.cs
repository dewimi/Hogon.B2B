using System;
using System.Collections.Generic;

namespace Hogon.Store.Models.Entities.Security
{
    /// <summary>
    /// 权限
    /// </summary>
    public class Authority
    {
        public Authority()
        {
            Rela_Authority_Function = new HashSet<Rela_Authority_Function>();
        }

        /// <summary>
        /// 权限Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public Menu Menu { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// 权限功能关联
        /// </summary>
        public virtual ICollection<Rela_Authority_Function> Rela_Authority_Function { get; set; }
    }
}
