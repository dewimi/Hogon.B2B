using Hogon.Framework.Core.UnitOfWork.EntityFramework;
using Hogon.Store.Models.Entities.MemberMan;
using Hogon.Store.Models.Entities.Security;

namespace Hogon.Store.Models.Entities.HRMan
{
    public class Staff : BaseEntity
    {
        /// <summary>
        /// 企业账号
        /// </summary>
        public Enterprise Enterprise { get; set; }

        /// <summary>
        /// 个人账号
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public Role Role { get; set; }
    }
}
