using AutoMapper;
using Hogon.Framework.Core.Common;
using Hogon.Framework.Core.Owin;
using Hogon.Framework.Web.Controller;
using System;
using System.Linq;
using System.Web.Mvc;
using Hogon.Framework.Utilities.SmartList;
using Hogon.Store.UserInterface.Admin.Areas.Security.Models.User;
using Hogon.Store.Models.Dto.Security;
using Hogon.Store.Services.ApplicationServices.MemberManContext;
using Hogon.Store.Models.Dto.MemberMan;
using Hogon.Store.Models.Entities.MemberMan;

namespace Hogon.Store.UserInterface.Admin.Areas.Security.Controllers
{
    public class UserController : SmartListController<AccountViewModel>
    {
        AccountApplicationService accountSvc = new AccountApplicationService();

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }

        public UserController(BaseViewRender<AccountViewModel> listRender) : base(listRender)
        {
        }

        // GET: Security/Personal
        public  ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit()
        {
            ViewBag.CreatedTime = DateTime.Now.ToShortDateString();
            ViewBag.UpdatedTime = DateTime.Now.ToShortDateString();
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(DtoAccount dtoAccount)
        {
            accountSvc.SaveUser(dtoAccount);
            return Json("");
        }

        /// <summary>
        /// 查询所有用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAllUsers()
        {
            var users = GetAllModels();
            return Json(users);
        }

        protected override IQueryable<AccountViewModel> GetAllModels()
        {
            var dtoMenu = accountSvc.GetAllAccount().OrderByDescending(m => m.CreateTime);

            var viewModels = dtoMenu.ConvertTo<DtoAccount, AccountViewModel>();

            return viewModels;
        }

        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IsExist(string userName)
        {
           int result = accountSvc.IsExist(userName);

            return Json(result);
        }

        /// <summary>
        /// 根据Id查询用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetUserById(Guid id)
        {
            var accounts = accountSvc.GetAccountById(id);
            Mapper.Initialize(cfg => cfg.CreateMap<DtoAccount, AccountViewModel>());
            var menuData = Mapper.Map<AccountViewModel>(accounts);
            return Json(menuData);
        }

        /// <summary>
        /// 根据用户名称查询相应菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAuthorityByUserId(string userName)
        {
            if (userName == null)
            {
                if (UserState.Current == null)
                {
                    return Redirect("/Account/Login");
                }
                else
                {
                    var menus = accountSvc.GetAuthorityByAccountId(UserState.Current.UserId);
                    return Json(menus);
                }
            }
            else
            {
                var menus = accountSvc.GetAuthorityByAccountId(UserState.Current.UserId);
                return Json(menus);
            }
          
        }

        /// <summary>
        /// 根据用户名称查询角色
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetRoleByUserName()
        {
            if (UserState.Current == null)
            {
                return Redirect("/Account/Login");
            }
            else
            {
                var role = accountSvc.GetRoleByAccountId(UserState.Current.UserId);
                return Json(role);

            }
            
        }
        
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public ActionResult GetUserInfo(string userInfo)
        {
           var dtoAccount = accountSvc.GetUserInfo(userInfo);
            return Json(dtoAccount);
        }

        /// <summary>
        /// 保存用户信息到企业下
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult SaveUserInfo(Guid id)
        {
            accountSvc.SaveUserInfo(id);
            return Json("");
        }

        /// <summary>
        /// 获取企业下的所有角色
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRole()
        {
            var roles = accountSvc.GetRole();

            return Json(roles);
        }

        /// <summary>
        /// 添加员工账号
        /// </summary>
        /// <param name="person"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public ActionResult AddAccount(DtoPerson person,Guid roleId)
        {
            accountSvc.AddAccount(person,roleId);
            return Json("");
        }
    }
}