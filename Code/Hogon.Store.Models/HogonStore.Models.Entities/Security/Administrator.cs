using System;
using System.Collections.Generic;
using Hogon.Store.Models.Entities.MemberMan;
using System.Linq;

namespace Hogon.Store.Models.Entities.Security
{
    /// <summary>
    /// 超级管理员角色
    /// </summary>
    public class Administrator : IRole
    {
        private Account _primaryAccount; // 角色从属主体
        private IEnumerable<Function> _authroizedFunctions;
        private IEnumerable<Menu> _authroizedMenus;

        public Administrator(Account primaryAccount
            , IEnumerable<Function> authroizedFunctions
            , IEnumerable<Menu> authroizedMenus)
        {
            _primaryAccount = primaryAccount;
            _authroizedFunctions = authroizedFunctions;
            _authroizedMenus = authroizedMenus;
        }

        public Guid Id
        {
            get
            {
                return Guid.Empty;
            }
        }

        public string CreatePerson
        {
            get
            {
                return "系统";
            }
        }

        public DateTime CreateTime
        {
            get
            {
                return PrimaryAccount.CreateTime;
            }
        }

        public string UpdatePerson
        {
            get
            {
                return "系统";
            }
        }

        public DateTime UpdateTime
        {
            get
            {
                return PrimaryAccount.CreateTime;
            }
        }

        public Account PrimaryAccount
        {
            get
            {
                return _primaryAccount;
            }
        }

        public bool IsAdministrator
        {
            get
            {
                return true;
            }
        }

        public bool IsEnable
        {
            get
            {
                return true;
            }
        }

        public string RoleName
        {
            get
            {
                return "超级管理员";
            }
        }

        public IEnumerable<Function> GetAuthorizedFunctions()
        {
            return _authroizedFunctions;
        }

        public IEnumerable<Function> GetAuthorizedFunctions(Guid menuId)
        {
            return _authroizedFunctions.Where(m => m.Menu.Id == menuId);
        }

        public IEnumerable<Menu> GetAuthroizedMenus()
        {
            return _authroizedMenus;
        }
    }
}
