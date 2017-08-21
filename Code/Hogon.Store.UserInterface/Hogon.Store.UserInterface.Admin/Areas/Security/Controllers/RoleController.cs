using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hogon.Framework.Core.Common;
using Hogon.Framework.Web.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Hogon.Framework.Utilities.SmartList;
using Hogon.Store.Services.ApplicationServices.SecurityContext;
using Hogon.Store.Models.Dto.Security;
using Hogon.Store.Models.Dto.Common;
using Hogon.Store.UserInterface.Admin.Areas.Security.Models.Role;

namespace Hogon.Store.UserInterface.Admin.Areas.Security.Controllers
{
    public class RoleController : SmartListController<RoleViewModel>
    {
        RoleApplicationService roleSvc = new RoleApplicationService();
        MenuApplicationService menuSvc = new MenuApplicationService();

        public RoleController(BaseViewRender<RoleViewModel> listRender) : base(listRender)
        {
        }

        // GET: Role/Role

        public ActionResult Index()
        {
            ViewBag.CreatedTime = DateTime.Now.ToShortDateString();
            ViewBag.UpdatedTime = DateTime.Now.ToShortDateString();

            return View();
        }

        public ActionResult RoleIndex()
        {
            return View();
        }

        /// <summary>
        /// 保存角色
        /// </summary>
        /// <returns></returns>
        public ActionResult AddRole(DtoRole role)
        {
            roleSvc.AddRole(role);
            return Json("");
        }

        /// <summary>
        /// 查询所有角色
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllRole()
        {
            var dtoRoles = roleSvc.GetRole();
            Mapper.Initialize(cfg => cfg.CreateMap<DtoRole, RoleViewModel>());
            var viewModels = dtoRoles.ProjectTo<RoleViewModel>();

            return Json(viewModels);
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetRoleByName(string roleName)
        {
           var role = roleSvc.GetRoleByName(roleName);

            return Json(role);
        }

        /// <summary>
        /// 根据角色Id查询权限菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMenuByRoleId(Guid roleId)
        {
           var menus = roleSvc.GetMenusByRoleId(roleId);
            return Json(menus);
        }

        /// <summary>
        /// 根据RoleId查询
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetRoleById(Guid roleId)
        {
            var role = roleSvc.GetRoleById(roleId);
            Mapper.Initialize(cfg => cfg.CreateMap<DtoRole, RoleViewModel>());
            var roleData = Mapper.Map<RoleViewModel>(role);

            return Json(roleData);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteRole(Guid roleId)
        {
            roleSvc.RemoveRole(roleId);
            return Json("");
        }

        /// <summary>
        /// 保存角色权限
        /// </summary>
        /// <param name="listTreeNode"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveAuthority(List<DtoTreeNode> listTreeNode, Guid roleId,
                 List<Guid> userNode, List<DtoTreeNode> unCheckedNode,List<Guid> unCheckUsers)
        {
            if (roleId == null)
            {
                roleId = new Guid();//"当前登录账号";
            }
            else
            {
                roleSvc.AddRoleAuthority(listTreeNode, roleId, userNode, unCheckedNode, unCheckUsers);
            }
          
            return Json("");
        }

        /// <summary>
        /// 根据角色Id查询对应用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetUsersByRoleId(Guid roleId)
        {
           var users = roleSvc.GetUsersByRoleId(roleId);
            return Json(users);
        }

        protected override IQueryable<RoleViewModel> GetAllModels()
        {
            var dtoRoles = roleSvc.GetRole().OrderByDescending(m => m.CreateTime);

            var viewModels = dtoRoles.ConvertTo<DtoRole, RoleViewModel>();

            return viewModels;
        }
    }
}