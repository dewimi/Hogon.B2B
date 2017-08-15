using System;
using System.Collections.Generic;

namespace Hogon.Store.Models.Entities.Security
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User
    {
        public User()
        {
            CreatedTime = DateTime.Now;
            Rela_Role_User = new HashSet<Rela_Role_User>();
        }

        /// <summary>
        /// 编号
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

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

        /// <summary>
        /// 用户角色关联集合
        /// </summary>
        public virtual ICollection<Rela_Role_User> Rela_Role_User { get; set; }
    }
}
