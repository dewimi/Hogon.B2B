using Hogon.Store.Models.Entities.MemberMan;
using Hogon.Store.Models.Entities.Security;
using Hogon.Store.Repositories.Security;
using System.Collections.Generic;
using System.Linq;

namespace Hogon.Store.Services.DomainServices.SecurityContext
{
    public class AuthorizationDomainService
    {
        MenuRepository _menuRepository = new MenuRepository();

        /// <summary>
        /// 根据用户获取可用的菜单集合
        /// </summary>
        public IEnumerable<Menu> GetAvailableMenusByUser(Account account)
        {
            return account.GetAvailableMenus();
        }

        /// <summary>
        /// 根据用户获取可用的功能集合
        /// </summary>
        public IEnumerable<Function> GetAvailableFunctionsByUser(Account account)
        {
            return account.GetAvailableFunctions();
        }
    }
}