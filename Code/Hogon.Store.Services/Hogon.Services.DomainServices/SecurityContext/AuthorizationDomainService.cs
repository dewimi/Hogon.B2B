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
        public IEnumerable<Menu> GetAvailableMenusByUser(User user)
        {
            if (user.Rela_Role_Account.Select(m => m.Role)
                .Where(m => m.IsAdministrator).Count() > 0)
            {
                return _menuRepository.FindBy(m => m.IsEnable);
            }
            else
            {
                return user.Rela_Role_Account.SelectMany
                    (m => m.Role.GetAuthroizedMenus());
            }
        }

        /// <summary>
        /// 根据用户获取可用的功能集合
        /// </summary>
        public IEnumerable<Function> GetAvailableFunctionsByUser(User user)
        {
            if (user.Rela_Role_Account.Select(m => m.Role)
                .Where(m => m.IsAdministrator).Count() > 0)
            {
                var menus = _menuRepository.FindBy(m => m.IsEnable).ToList();
                var funcs = menus.SelectMany(m => m.Functions);

                return funcs;
            }
            else
            {
                return user.Rela_Role_Account.SelectMany
                    (m => m.Role.GetAuthroizedFunctions());
            }
        }
    }
}