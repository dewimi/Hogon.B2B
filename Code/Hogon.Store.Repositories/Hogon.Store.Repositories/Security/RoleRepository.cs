using Hogon.Framework.Core.UnitOfWork.EntityFramework;
using Hogon.Store.Models.Entities.Security;

namespace Hogon.Store.Repositories.Security
{
    public class RoleRepository : EFRepository<Role>
    {
        /// <summary>
        /// 删除角色下的用户
        /// </summary>
        /// <param name="relaRoleUser"></param>
        /// <returns></returns>
        public Rela_Role_Person DeleteRela_Role_User(Rela_Role_Person relaRoleUser)
        {
            return DeleteObject(relaRoleUser);
        }
    }
}
