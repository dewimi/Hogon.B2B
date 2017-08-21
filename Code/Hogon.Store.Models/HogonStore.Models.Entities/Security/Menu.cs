using Hogon.Framework.Core.UnitOfWork.EntityFramework;
using System;
using System.Collections.Generic;

namespace Hogon.Store.Models.Entities.Security
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class Menu : BaseEntity
    {
        public Menu()
        {
            Children = new HashSet<Menu>();
            Functions = new HashSet<Function>();
            Authority = new HashSet<Authority>();


        }


        /// <summary>
        /// 父菜单Id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 成员名称
        /// </summary>
        private string memberName{ get; set; }

        /// <summary>
        /// 菜单编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// 小图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 菜单是否可用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 菜单功能集合
        /// </summary>
        public virtual ICollection<Function> Functions { get; set; }

        /// <summary>
        /// 父级菜单集合
        /// </summary>
        public virtual Menu Parent { get; set; }

        /// <summary>
        /// 子级菜单集合
        /// </summary>
        public virtual ICollection<Menu> Children { get; set; }

        /// <summary>
        /// 权限集合
        /// </summary>
        public virtual ICollection<Authority> Authority { get; set; }
    }
}
