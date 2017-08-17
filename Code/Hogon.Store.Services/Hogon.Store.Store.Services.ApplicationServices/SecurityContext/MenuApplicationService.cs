using AutoMapper;
using Hogon.Framework.Core.Common;
using Hogon.Framework.Core.Owin;
using Hogon.Framework.Core.UnitOfWork;
using Hogon.Store.Models.Dto.Common;
using Hogon.Store.Models.Dto.Security;
using Hogon.Store.Models.Entities.Security;
using Hogon.Store.Repositories.MemberMan;
using Hogon.Store.Repositories.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hogon.Store.Services.ApplicationServices.SecurityContext
{
    public class MenuApplicationService : BaseApplicationService
    {
        MenuRepository menuReps;
        AccountRepository accountReps = new AccountRepository();
        RoleRepository roleReps = new RoleRepository();
        public MenuApplicationService()
        {
            menuReps = new MenuRepository();

        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        public IQueryable<DtoMenu> GetMenus()
        {

            var menus = menuReps.FindAll();
            //var dtoMenus = menus.ConvertTo<Menu, DtoMenu>();
            var dtoMenus = menus.Select(m => new DtoMenu()
            {
                Id = m.Id,
                Code = m.Code,
                Name = m.Name,
                ParentId = m.ParentId,
                ParentName = m.Parent.Name,
                URL = m.URL,
                Icon = m.Icon,
                Sort = m.Sort,
                IsEnable = m.IsEnable,
                CreateTime = m.CreateTime,
                CreatePerson = m.CreatePerson,
                UpdatePerson = m.UpdatePerson,
                UpdateTime = m.UpdateTime

            });

            return dtoMenus;
        }

        /// <summary>
        /// 遍历所有菜单节点
        /// </summary>
        /// <returns></returns>
        public List<DtoTreeNode> GetTreeMenus(string userName)
        {

            var allMenus = menuReps.FindAll().OrderBy(m=>m.Sort).ToList();
            //根据用户id查询相应的权限菜单
            var menu = accountReps.FindBy(r => r.Name == userName).SelectMany
                   (r => r.Rela_Role_Account).Select(r => r.Role);
            var menus = menu.SelectMany(a => a.Authorities).Select(m => m.Menu).OrderBy(m=>m.Sort);
            //查询菜单下有权限的按钮
            var generalFunc = accountReps.FindBy(r => r.Name == userName).SelectMany
                   (r => r.Rela_Role_Account).Select(r => r.Role).SelectMany(a => a.Authorities)
                   .SelectMany(r => r.Rela_Authority_Function);

            //获取遍历出来的菜单节点
            var treeNodeLists = CreateNodeList(allMenus.AsQueryable(), menus, generalFunc);

            return treeNodeLists;

        }

        /// <summary>
        /// 点击角色查看相应权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<DtoTreeNode> MenuByRoleId(Guid roleId)
        {

            var allMenus = menuReps.FindAll().ToList();

            //查询角色相应的权限菜单
            var menus = roleReps.FindBy(r => r.Id == roleId).SelectMany(a => a.Authorities).Select(m => m.Menu);

            //查询角色相应的权限按钮
            var generalFunc = roleReps.FindBy(r => r.Id == roleId).SelectMany(a => a.Authorities)
                  .SelectMany(r => r.Rela_Authority_Function);
            //获取遍历出来的菜单节点
            var treeNodeList = CreateNodeList(allMenus.AsQueryable(), menus, generalFunc);

            return treeNodeList;

        }
        /// <summary>
        /// 生成菜单集合
        /// </summary>
        /// <param name="allMenus"></param>
        /// <param name="menus"></param>
        /// <param name="generalFunc"></param>
        /// <returns></returns>
        public List<DtoTreeNode> CreateNodeList(IQueryable<Menu> allMenus,
            IQueryable<Menu> menus, IQueryable<Rela_Authority_Function> generalFunc)
        {

            List<DtoTreeNode> nodeList = new List<DtoTreeNode>();
            var parentMenus = allMenus.Where(m => m.ParentId == null).OrderBy(m=>m.Sort);

            // 遍历一级菜单
            foreach (var parentMenu in parentMenus)
            {
                var parentNode = new DtoTreeNode
                {
                    Id = parentMenu.Id,
                    ParentId = null,
                    text = parentMenu.Name,
                    URL = parentMenu.URL,
                    Icon = parentMenu.Icon,
                    Sort = parentMenu.Sort
                };

                // 根据一级菜单Id查找二级菜单
                var children = allMenus.Where(m => m.ParentId == parentMenu.Id);
                var childrenNodes = children.Select(m => new DtoTreeNode
                {
                    Id = m.Id,
                    ParentId = m.ParentId,
                    text = m.Name,
                    URL = m.URL,
                    Icon = m.Icon,
                    Sort = m.Sort
                });

                parentNode.nodes = childrenNodes.ToList();

                //三级菜单

                foreach (var childNode in parentNode.nodes)
                {
                    var funcs = allMenus.SelectMany(m => m.Functions).Where(n=>n.Menu.Id == childNode.Id);
                    var generalFuncNodes = funcs.Select(g => new DtoTreeNode
                    {
                        Id = g.Id,
                        text = g.Name,

                    }).ToList();
                    foreach (var item in generalFuncNodes)
                    {
                        item.state.Add("isFunction", true);
                    }
                    // 按钮选中
                    //foreach (var general in generalFunc)
                    //{
                    //    foreach (var gFunctionNodes in generalFuncNodes)
                    //    {
                    //        if (gFunctionNodes.Id == general.Function.Id)
                    //        {
                    //            gFunctionNodes.state.Remove("checked");
                    //            gFunctionNodes.state.Add("checked", true);

                    //        }
                    //    }
                    //}

                    childNode.nodes = generalFuncNodes.ToList();
                }
                // 菜单选中
                foreach (var menu in menus)
                {
                    if (parentNode.text == menu.Name)
                    {
                        parentNode.state.Remove("checked");
                        parentNode.state.Add("checked", true);
                    }
                    foreach (var child in parentNode.nodes)
                    {
                        if (child.text == menu.Name)
                        {
                            child.state.Remove("checked");
                            child.state.Add("checked", true);
                        }
                    }
                }

                nodeList.Add(parentNode);
            }

            return nodeList;
        }
        /// <summary>
        /// 获取通用功能
        /// </summary>
        /// <returns></returns>
        public IQueryable<DtoFunction> GetFunction(Guid menuId)
        {
            var functions = menuReps.FindBy(i => i.Id == menuId)
                .SelectMany(m => m.Functions).OfType<GeneralFunction>();

            var dtoFunctions = functions.ConvertTo<Function, DtoFunction>();

            return dtoFunctions;
        }

        /// <summary>
        /// 获取自定义功能
        /// </summary>
        /// <returns></returns>
        public IQueryable<DtoFunction> GetMenuFunctions(Guid menuId)
        {
            var functions = menuReps.FindBy(i => i.Id == menuId)
                .SelectMany(m => m.Functions).OfType<CustomFunction>();

            var dtoFunctions = functions.ConvertTo<Function, DtoFunction>();

            return dtoFunctions;
        }

        /// <summary>
        /// 保存基本信息
        /// </summary>
        /// <param name="dtoMenu"></param>
        public Guid SaveBasic(DtoMenu dtoMenu, List<DtoFunction> generalFunctions)
        {
            if (dtoMenu.Id == new Guid())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DtoMenu, Menu>());
                var menu = Mapper.Map<Menu>(dtoMenu);
                menu.Id = Guid.NewGuid();

                menuReps.Add(menu);

                SaveGeneralPermissions(generalFunctions, menu);

                Commit();
                return menu.Id;
            }
            else
            {
                var menu = menuReps.FindBy(i => i.Id == dtoMenu.Id).First();

                Mapper.Initialize(cfg => cfg.CreateMap<DtoMenu, Menu>());
                Mapper.Map(dtoMenu, menu);

                SaveGeneralPermissions(generalFunctions, menu);

                Commit();
                return menu.Id;
            }
        }

        /// <summary>
        /// 保存通用功能
        /// </summary>
        protected void SaveGeneralPermissions(List<DtoFunction> dtoGeneralFunctions, Menu menu)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DtoFunction, GeneralFunction>());

            foreach (var func in menu.Functions)
            {
                if (dtoGeneralFunctions.Where(m => m.Name == func.Name).Count() == 0)
                {
                    menu.Functions.Remove(func);
                }
            }

            foreach (var dtoFunc in dtoGeneralFunctions)
            {
                if (menu.Functions.Where(m => m.Name == dtoFunc.Name).Count() == 0)
                {
                    var func = Mapper.Map<GeneralFunction>(dtoFunc);
                    func.CreatePerson = UserState.Current.UserName;
                    func.CreateTime = DateTime.Now;
                    func.UpdatePerson = UserState.Current.UserName;
                    func.UpdateTime = DateTime.Now;
                    func.Id = Guid.NewGuid();
                    menu.Functions.Add(func);
                }
            }
        }

        /// <summary>
        /// 保存自定义功能
        /// </summary>
        public void SaveMenuPermissions(DtoFunction dtoFunction, Guid menuId)
        {
            var menu = menuReps.FindBy(m => m.Id == menuId).First();
            CustomFunction func = null;

            if (dtoFunction.Id == new Guid())
            {
                func = new CustomFunction();
                Mapper.Initialize(cfg => cfg.CreateMap<DtoFunction, CustomFunction>());
                func = Mapper.Map<CustomFunction>(dtoFunction);
                func.Id = Guid.NewGuid();
                menu.Functions.Add(func);

            }
            else
            {

                func = menuReps.FindBy(i => i.Id == menuId).SelectMany(m => m.Functions)
                    .Where(i => i.Id == dtoFunction.Id).OfType<CustomFunction>().First();

                Mapper.Initialize(cfg => cfg.CreateMap<DtoFunction, CustomFunction>());
                Mapper.Map(dtoFunction, func);
            }

            Commit();
        }

        /// <summary>
        /// 根据Id查询菜单功能
        /// </summary>
        /// <returns></returns>
        public DtoFunction GetMenuFunctionById(DtoFunction dtoFunction, Guid menuId)
        {

            if (dtoFunction.Id == new Guid())
            {
                var func = menuReps.FindBy(i => i.Id == menuId).SelectMany(m => m.Functions);
                Mapper.Initialize(cfg => cfg.CreateMap<Function, DtoFunction>());
                var dtoFunc = Mapper.Map<DtoFunction>(func);

                return dtoFunc;

            }
            else
            {
                var func = menuReps.FindBy(i => i.Id == menuId).SelectMany(m => m.Functions)
                      .Where(i => i.Id == dtoFunction.Id).First();
                Mapper.Initialize(cfg => cfg.CreateMap<Function, DtoFunction>());
                var dtoFunc = Mapper.Map<DtoFunction>(func);

                return dtoFunc;
            }
        }

        /// <summary>
        /// 删除菜单功能
        /// </summary>
        public void DeleteMenuFunction(Guid id, Guid menuId)
        {
            var menu = menuReps.FindBy(m => m.Id == menuId).First();
            var func = menu.Functions.Where(f => f.Id == id).First();

            menuReps.DeleteFunction(func);

            Commit();
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuId"></param>
        public void DeleteMenu(Guid menuId)
        {
            var menu = menuReps.FindBy(m => m.Id == menuId).First();
            DeleteChildrenMenus(menu);
            menuReps.Remove(menu);

            Commit();
        }

        /// <summary>
        /// 递归删除子级菜单
        /// </summary>
        /// <param name="menu"></param>
        public void DeleteChildrenMenus(Menu menu)
        {
            int childrenCount = menu.Children.Count();

            if (childrenCount > 0)
            {
                for (int i = 0; i < childrenCount; ++i)
                {
                    var child = menu.Children.Last();
                    DeleteChildrenMenus(child);
                    menuReps.Remove(child);
                }
            }
        }

        /// <summary>
        /// 根据菜单Id查询
        /// </summary>
        /// <returns></returns>
        public DtoMenu GetMenuById(Guid menuId)
        {
            var menu = menuReps.FindBy(i => i.Id == menuId).First();
            Mapper.Initialize(cfg => cfg.CreateMap<Menu, DtoMenu>());
            var menuData = Mapper.Map<DtoMenu>(menu);
            if(menu.Parent != null)
            {
                menuData.ParentName = menu.Parent.Name;
            }

            return menuData;

        }
    }
}