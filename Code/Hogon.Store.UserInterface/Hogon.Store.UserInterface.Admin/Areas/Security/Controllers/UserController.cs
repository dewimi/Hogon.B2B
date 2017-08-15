using AutoMapper;
using Hogon.Framework.Core.Common;
using Hogon.Framework.Core.Owin;
using Hogon.Framework.Web.Controller;
using System;
using System.Linq;
using System.Web.Mvc;
using Hogon.Framework.Utilities.SmartList;
using Hogon.Store.UserInterface.Admin.Areas.Security.Models.User;
using Hogon.Store.Services.ApplicationServices.SecurityContext;
using Hogon.Store.Models.Dto.Security;
using Hogon.Framework.Core.Owin.Authrization;

namespace Hogon.Store.UserInterface.Admin.Areas.Security.Controllers
{
    public class UserController : SmartListController<UserViewModel>
    {
        UserApplicationService userSvc = new UserApplicationService();

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }

        public UserController(BaseViewRender<UserViewModel> listRender) : base(listRender)
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

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(DtoUser dtoUser)
        {
            userSvc.SaveUser(dtoUser);
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
        protected override IQueryable<UserViewModel> GetAllModels()
        {
            var dtoMenu = userSvc.GetAllUser().OrderByDescending(m => m.CreatedTime);

            var viewModels = dtoMenu.ConvertTo<DtoUser, UserViewModel>();

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
           int result = userSvc.IsExist(userName);

            return Json(result);
        }

        /// <summary>
        /// 根据Id查询用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetUserById(Guid id)
        {
            var users = userSvc.GetUserById(id);
            Mapper.Initialize(cfg => cfg.CreateMap<DtoUser, UserViewModel>());
            var menuData = Mapper.Map<UserViewModel>(users);
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
                    var menus = userSvc.GetAuthorityByUserId(UserState.Current.UserId);
                    return Json(menus);
                }
            }
            else
            {
                var menus = userSvc.GetAuthorityByUserId(UserState.Current.UserId);
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
                var role = userSvc.GetRoleByUserId(UserState.Current.UserId);
                return Json(role);

            }
            
        }

    }
}