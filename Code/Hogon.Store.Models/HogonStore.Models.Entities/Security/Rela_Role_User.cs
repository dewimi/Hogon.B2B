using System;

namespace Hogon.Store.Models.Entities.Security
{
    /// <summary>
    /// 用户角色关联
    /// </summary>
    public class Rela_Role_User
    {
        public Rela_Role_User()
        {
        }

        /// <summary>
        ///Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdatedTime { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 创建人Id
        /// </summary>
        public int CreatorId { get; set; }
        /// <summary>
        /// 修改人Id
        /// </summary>
        public int UpdaterId { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public Role Role { get; set; }
    }
}
