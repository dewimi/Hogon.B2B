using Hogon.Store.Models.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hogon.Store.Models.Entities.MemberMan;
using Hogon.Store.Repositories.Security;

namespace Hogon.Store.Models.Module.Security
{
    public class RoleFactory : IRoleFactory
    {
        private MenuRepository _menuReps = new MenuRepository();

        public IRole Create(Account account)
        {
            IRole role = null;

            if (account.CurrentIdentity == account)
            {
                // 如果是已当前账户作为身份，则获取超级管理员角色
                var primaryAccount = this;
                var authorizedMenus = _menuReps.FindBy(m => m.IsEnable == true).ToList();
                var authorizedFunctions = authorizedMenus
                    .SelectMany(m => m.Functions).Where(m => m.IsEnable == true);

                role = new Administrator(account, authorizedFunctions, authorizedMenus);
            }
            else
            {
                // 如果是企业员工身份，则获取企业管理员配置的角色
                Person person = (Person)account;

                role = person.GetCurrentStaff().Role;
            }

            return role;
        }
    }
}
