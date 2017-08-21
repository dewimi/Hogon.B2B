using AutoMapper;
using Hogon.Framework.Core.Common;
using Hogon.Framework.Core.Owin;
using Hogon.Framework.Web.Controller;
using Hogon.Store.Models.Dto.Security;
using Hogon.Store.UserInterface.WebSite.Areas.Security.Models.Personal;
using System;
using System.Linq;
using System.Web.Mvc;
using Hogon.Framework.Utilities.SmartList;
using Hogon.Store.Services.ApplicationServices.MemberManContext;
using Hogon.Store.Models.Dto.MemberMan;

namespace Hogon.Store.UserInterface.WebSite.Areas.Security.Controllers
{
    public class PersonalController : SmartListController<AccountViewModel>
    {
        AccountApplicationService _accountSvc = new AccountApplicationService();

        public PersonalController(BaseViewRender<AccountViewModel> listRender) : base(listRender)
        {
        }

        // GET: Security/Personal
        public  ActionResult Index()
        {
            return View();
        }
       
        public ActionResult Add()
        {
            ViewBag.CreatedTime = DateTime.Now.ToShortDateString();
            ViewBag.UpdatedTime = DateTime.Now.ToShortDateString();
            return View();
        }

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(DtoAccount dtoAccount)
        {
            _accountSvc.SaveUser(dtoAccount);
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
            var dtoAccounts = _accountSvc.GetAllAccount().OrderByDescending(m => m.CreateTime);

            var viewModels = dtoAccounts.ConvertTo<DtoAccount, AccountViewModel>();

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
           int result = _accountSvc.IsExist(userName);

            return Json(result);
        }

        /// <summary>
        /// 根据Id查询用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetUserById(Guid id)
        {
            var users = _accountSvc.GetAccountById(id);
            Mapper.Initialize(cfg => cfg.CreateMap<DtoAccount, AccountViewModel>());
            var menuData = Mapper.Map<AccountViewModel>(users);
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
                    userName = UserState.Current.UserName;
                    var menus = _accountSvc.GetAuthorityByAccountId(UserState.Current.AccountId);
                    return Json(menus);
                }
            }
            else
            {
                var menus = _accountSvc.GetAuthorityByAccountId(UserState.Current.AccountId);
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
                var role = _accountSvc.GetRoleByAccountId(UserState.Current.AccountId);
                return Json(role);

            }
            
        }

    }
}