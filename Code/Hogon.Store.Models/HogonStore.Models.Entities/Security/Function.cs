using Hogon.Framework.Core.UnitOfWork.EntityFramework;
using System.Collections.Generic;

namespace Hogon.Store.Models.Entities.Security
{
    /// <summary>
    /// 功能
    /// </summary>
    public abstract class Function : BaseEntity
    {
        public Function()
        {
            Rela_Authority_Function = new HashSet<Rela_Authority_Function>();
        }

        /// <summary>
        /// 功能名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 功能编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 功能是否可用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public virtual Menu Menu { get; set; }

        /// <summary>
        /// 权限功能关联
        /// </summary>
        public virtual ICollection<Rela_Authority_Function> Rela_Authority_Function { get; set; }
    }
}
