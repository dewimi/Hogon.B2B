using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.Dto.Security
{
    public class DtoAuthority
    {
        /// <summary>
        /// 权限Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 菜单Id
        /// </summary>
        public Guid MenuId { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        public Guid RoleId { get; set; }
    }
}
