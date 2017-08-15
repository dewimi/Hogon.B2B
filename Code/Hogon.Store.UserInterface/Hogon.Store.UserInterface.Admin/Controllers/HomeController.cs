using System.Linq;
using System.Web.Mvc;
using Hogon.Framework.Web.Controller;
using Hogon.Framework.Core.Common;
using System;
using Hogon.Framework.Utilities.SmartList;
using Hogon.Store.UserInterface.Admin.Models.Home;
using Hogon.Store.Services.ApplicationServices.SecurityContext;
using Hogon.Store.Models.Dto.Security;

namespace Hogon.Store.UserInterface.Admin.Controllers
{
    [Authorize]
    public class HomeController : SmartListController<MenuViewModel>
    {
        MenuApplicationService menuSvc = new MenuApplicationService();

        public HomeController(BaseViewRender<MenuViewModel> listRender) : base(listRender)
        {
        }

        public ActionResult Index()
        {
            return View();
        }
        
        /// <returns></returns>
        public ActionResult Home()
        {
            return View();
        }

        /// <summary>
        /// 查询所有菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BindingMenu()
        {
            var menus = GetAllModels();
          
            return Json(menus);
        }

        protected override IQueryable<MenuViewModel> GetAllModels()
        {
            var dtoMenu = menuSvc.GetMenus();
            var viewModels = dtoMenu.ConvertTo<DtoMenu, MenuViewModel>();

            return viewModels;
        }
    }
}