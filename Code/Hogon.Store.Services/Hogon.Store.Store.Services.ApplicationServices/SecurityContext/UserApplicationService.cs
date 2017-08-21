using System.Web.Security;
using System;
using AutoMapper;
using System.Linq;
using Hogon.Store.Repositories.Security;
using Hogon.Store.Models.Entities.Security;
using Hogon.Store.Models.Dto.Security;
using Hogon.Framework.Core.UnitOfWork;
using Hogon.Framework.Core.Owin;
using Hogon.Framework.Core.Common;
using Hogon.Store.Repositories.MemberMan;
using Hogon.Store.Models.Entities.MemberMan;

namespace Hogon.Store.Services.ApplicationServices.SecurityContext
{
    //public class UserApplicationService : BaseApplicationService
    //{
    //    AuthorizationDomainService _authorizationService = new AuthorizationDomainService();
    //    AccountRepository _accountReps = new AccountRepository();
    //    Md5Encryptor _encryptor = new Md5Encryptor();

    //    public UserApplicationService()
    //    {
    //    }

    //    /// <summary>
    //    /// 登录
    //    /// </summary>
    //    /// <param name="userName"></param>
    //    /// <param name="password"></param>
    //    /// <returns></returns>
    //public bool Login(string userName, string password)
    //{
    //    // Check user login credential.
    //    var user = ValidateUser(userName, password);

    //    if (user == null)
    //    {
    //        return false;
    //    }

    //    // If input credential is available, set cookie.
    //    FormsAuthentication.SetAuthCookie(userName, false);

    //    UserState userState = new UserState()
    //    {
    //        UserId = user.Id,
    //        UserName = user.Name,
    //        NickName = user.NickName,
    //        Email = user.Email,
    //    };

    //    var availableRoles = user.Rela_Role_User.Select(m => m.Role)
    //        .Where(m => m.IsEnable == true).AsQueryable();
    //    userState.Roles = availableRoles.ConvertTo<Role, RoleState>().ToArray();

    //    userState.AvailableMenus = _authorizationService.GetAvailableMenusByUser(user)
    //        .AsQueryable().ConvertTo<Menu, MenuState>().ToArray();

    //    userState.AvailableFunctions = _authorizationService
    //        .GetAvailableFunctionsByUser(user).Select(m => new FunctionState()
    //        {
    //            Id = m.Id,
    //            Name = m.Name,
    //            MenuCode = m.Menu.Code,
    //            FunctionCode = m.Code,
    //            IsEnable = m.IsEnable,
    //        }).Where(m => m.IsEnable == true).ToArray();

    //    UserState.Current = userState;

    //    return true;
        //    }

        //    /// <summary>
        //    /// 登出
        //    /// </summary>
        //    /// <returns></returns>
        //    public void Logout()
        //    {
        //        FormsAuthentication.SignOut();
        //    }

        //    /// <summary>
        //    /// 保存用户信息
        //    /// </summary>
        //    public void SaveUser(DtoUser dtoUser)
        //    {
        //        if (dtoUser.Id == new Guid())
        //        {
        //            Mapper.Initialize(cfg => cfg.CreateMap<DtoUser, User>());

        //           var isExist = _accountReps.FindBy(m => m.Name == dtoUser.Name).FirstOrDefault();
        //            if (isExist == null)
        //            {
        //                // 如果密码是新密码，对密码进行加密
        //                dtoUser.Password = _encryptor.Encrypt(dtoUser.Password);

        //                var user = Mapper.Map<User>(dtoUser);
        //                user.Id = Guid.NewGuid();

        //                _accountReps.Add(user);
        //            }

        //        }
        //        else
        //        {
        //            var user = _accountReps.FindBy(i => i.Id == dtoUser.Id).First();

        //            // 如果密码是新密码，对密码进行加密
        //            if (user.Password != dtoUser.Password)
        //                dtoUser.Password = _encryptor.Encrypt(dtoUser.Password);

        //            Mapper.Initialize(cfg => cfg.CreateMap<DtoUser, User>());
        //            Mapper.Map(dtoUser, user);
        //        }

        //        Commit();
        //    }

        //    /// <summary>
        //    /// 查询所有用户
        //    /// </summary>
        //    /// <returns></returns>
        //    public IQueryable<DtoUser> GetAllUser()
        //    {
        //        var users = _accountReps.FindAll();

        //        var dtoUser = users.ConvertTo<User, DtoUser>();

        //        return dtoUser;
        //    }

        //    /// <summary>
        //    /// 判断用户名是否存在
        //    /// </summary>
        //    /// <returns></returns>
        //    public int IsExist(string userName)
        //    {
        //        int result = 0;
        //        var user = _accountReps.FindBy(i => i.Name == userName).FirstOrDefault();

        //        if (user == null)
        //        {
        //            result = 0;
        //        }
        //        else
        //        {
        //            result = 1;
        //        }
        //        return result;
        //    }

        //    /// <summary>
        //    /// 根据用户Id查询信息
        //    /// </summary>
        //    /// <returns></returns>
        //    public DtoUser GetAccountById(Guid Id)
        //    {
        //        var user = _accountReps.FindBy(i => i.Id == Id).First();
        //        Mapper.Initialize(cfg => cfg.CreateMap<User, DtoUser>());
        //        var users = Mapper.Map<DtoUser>(user);

        //        return users;
        //    }

        //    /// <summary>
        //    /// 用户名密码验证
        //    /// </summary>s
        //    /// <param name="userName"></param>
        //    /// <param name="password"></param>
        //    /// <returns></returns>
        //    private Account ValidateUser(string userName, string password)
        //    {
        //        password = _encryptor.Encrypt(password);

        //        // 使用加密后的密码进行比较
        //        var account = _accountReps.FindAll()
        //            .Where(u => u.Name == userName && u.Password == password)
        //            .FirstOrDefault();

        //        return account;
        //    }

        //    /// <summary>
        //    /// 根据用户查询相应权限
        //    /// </summary>
        //    /// <param name="userId"></param>
        //    /// <returns></returns>
        //    public IQueryable<DtoMenu> GetAuthorityByUserId(Guid userId)
        //    {
        //        var menus = UserState.Current.AvailableMenus.AsQueryable();

        //        var dtoMenus = menus.ConvertTo<MenuState, DtoMenu>();

        //        return dtoMenus;
        //    }

        //    /// <summary>
        //    /// 根据用户名称查询角色
        //    /// </summary>
        //    /// <param name="userName"></param>
        //    /// <returns></returns>
        //    public DtoRole GetRoleByUserId(Guid userId)
        //    {
        //        var role = _accountReps.FindBy(r => r.Id == userId).SelectMany
        //           (r => r.Rela_Role_User).Select(r => r.Role).First();
        //        Mapper.Initialize(cfg => cfg.CreateMap<Role, DtoRole>());
        //        var roleData = Mapper.Map<DtoRole>(role);

        //        return roleData;
        //    }
        //}
    }