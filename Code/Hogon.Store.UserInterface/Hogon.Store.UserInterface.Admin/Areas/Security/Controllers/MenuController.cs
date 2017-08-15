using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hogon.Framework.Core.Owin;
using Hogon.Framework.Web.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Hogon.Framework.Utilities.SmartList;
using Hogon.Store.UserInterface.Admin.Models.Home;
using Hogon.Store.Services.ApplicationServices.SecurityContext;
using Hogon.Store.Models.Dto.Security;

namespace Hogon.Store.UserInterface.Admin.Areas.Security.Controllers
{
    public class MenuController : SmartListController<MenuViewModel>
    {
        MenuApplicationService menuSvc = new MenuApplicationService();
        MenuFunctionApplicationService menufunctionSvc = new MenuFunctionApplicationService();

        public MenuController(BaseViewRender<MenuViewModel> listRender) : base(listRender)
        {
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }

        // GET: Menu
        public ActionResult Index()
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


        #region + 查询 根据Id查询
        /// <summary>
        /// 获取上级菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetPreviousMenu()
        {
            var menus = GetAllModels();
            return Json(menus);
        }

        /// <summary>
        /// 查询菜单功能
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMenuFunctions(Guid menuId)
        {
            var menufunction = menuSvc.GetMenuFunctions(menuId);
            return Json(menufunction);
        }

        /// <summary>
        /// 查询所有菜单
        /// </summary>
        /// <returns></returns>
        protected override IQueryable<MenuViewModel> GetAllModels()
        {
            var dtoMenu = menuSvc.GetMenus().OrderByDescending(m => m.CreateTime);
            Mapper.Initialize(cfg => cfg.CreateMap<DtoMenu, MenuViewModel>());
            var viewModels = dtoMenu.ProjectTo<MenuViewModel>();

            return viewModels;
        }

        /// <summary>
        /// 查询菜单权限
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTreeMenus()
        {
            if (UserState.Current != null)
            {
                string userName = UserState.Current.UserName;
                var menus = menuSvc.GetTreeMenus(userName);
                return Json(menus);
            }
            else
            {
                return Redirect("/Account/Login");
            }
            
        }

        /// <summary>
        /// 点击角色查看相应权限
        /// </summary>
        /// <returns></returns>
        public ActionResult MenusByRoleId(Guid roleId)
        {
           var menus = menuSvc.MenuByRoleId(roleId);
            return Json(menus);
        }

        /// <summary>
        /// 根据Id查询菜单功能
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMenuFunctionById(DtoFunction dtoMenuFunctions, Guid menuId)
        {

            var menuFcunc = menuSvc.GetMenuFunctionById(dtoMenuFunctions, menuId);
            return Json(menuFcunc);
        }

        /// <summary>
        /// 根据菜单Id查询信息
        /// </summary>
        [HttpPost]
        public ActionResult GetMenuById(Guid id)
        {
            var menu = menuSvc.GetMenuById(id);
            Mapper.Initialize(cfg => cfg.CreateMap<DtoMenu, MenuViewModel>());
            var menuData = Mapper.Map<MenuViewModel>(menu);

            return Json(menuData);

        }
        #endregion

        #region + 保存
        /// <summary>
        /// 保存基本信息
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(DtoMenu menu, List<DtoFunction> generalFunction)
        {
            var menuId = menuSvc.SaveBasic(menu, generalFunction);

            return Json(menuId);
        }

        /// <summary>
        /// 保存菜单功能
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveMenuFunction(DtoFunction dtoMenuFunctions, Guid menuId)
        {
            menuSvc.SaveMenuPermissions(dtoMenuFunctions, menuId);

            return Json("");
        }
        #endregion

        #region + 删除
        /// <summary>
        /// 删除功能菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteMenuFunction(Guid id, Guid menuId)
        {
            menuSvc.DeleteMenuFunction(id, menuId);
            return Json("");
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public ActionResult Remove(Guid Id)
        {
            menuSvc.DeleteMenu(Id);
            return Json("");
        } 
        #endregion

    }
}