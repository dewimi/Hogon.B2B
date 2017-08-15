using AutoMapper;
using Hogon.Framework.Core.Common;
using Hogon.Framework.Core.UnitOfWork;
using Hogon.Store.Models.Dto.Common;
using Hogon.Store.Models.Dto.Security;
using Hogon.Store.Models.Entities.Security;
using Hogon.Store.Repositories.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hogon.Store.Services.ApplicationServices.SecurityContext
{
    public class RoleApplicationService : BaseApplicationService
    {
        RoleRepository roleReps;
        MenuRepository menuReps;
        AuthorityRepository authorityReps = new AuthorityRepository();
        UserRepository userReps = new UserRepository();
        public RoleApplicationService()
        {
            roleReps = new RoleRepository();
            menuReps = new MenuRepository();
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        public void AddRole(DtoRole dtoRole)
        {
            if (dtoRole.Id == new Guid())
            {
                Role role = new Role();
                Mapper.Initialize(cfg => cfg.CreateMap<DtoRole, Role>());
                var roleData = Mapper.Map<Role>(dtoRole);
                roleData.Id = Guid.NewGuid();

                roleReps.Add(roleData);
            }
            else
            {
                var role = roleReps.FindBy(i => i.Id == dtoRole.Id).First();

                Mapper.Initialize(cfg => cfg.CreateMap<DtoRole, Role>());
                Mapper.Map<DtoRole, Role>(dtoRole, role);
            }
            Commit();
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <returns></returns>
        public IQueryable<DtoRole> GetRole()
        {
            var roles = roleReps.FindAll();

            var dtoRole = roles.ConvertTo<Role, DtoRole>();

            return dtoRole;
        }

        /// <summary>
        /// 根据角色Id查询菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IQueryable<DtoMenu> GetMenusByRoleId(Guid roleId)
        {
            var menus = roleReps.FindBy(r => r.Id == roleId)
                .SelectMany(r => r.Authorities).Select(m => m.Menu);

            var dtoMenus = menus.ConvertTo<Menu, DtoMenu>();

            return dtoMenus;
        }

        /// <summary>
        /// 根据Id查询角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public DtoRole GetRoleById(Guid roleId)
        {
            var role = roleReps.FindBy(i => i.Id == roleId).First();
            Mapper.Initialize(cfg => cfg.CreateMap<Role, DtoRole>());
            var roleData = Mapper.Map<DtoRole>(role);

            return roleData;
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public IQueryable<DtoRole> GetRoleByName(string roleName)
        {
            var roles = roleReps.FindAll();
            if (!string.IsNullOrEmpty(roleName))
            {
                roles = roles.Where(r => r.RoleName == roleName);
            }
            var dtoRole = roles.ConvertTo<Role, DtoRole>();
            return dtoRole;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        public void RemoveRole(Guid roleId)
        {
            Role role = roleReps.FindBy(u => u.Id == roleId).FirstOrDefault();

            roleReps.Remove(role);

            Commit();
        }

        /// <summary>
        /// 添加角色权限
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="roleId"></param>
        public void AddRoleAuthority(List<DtoTreeNode> listTreeNode, Guid roleId, List<Guid> userNode,
            List<DtoTreeNode> unCheckedNode, List<Guid> unCheckUsers)
        {

            //保存用户选择的权限
            if (listTreeNode != null)
            {
                foreach (var menu in listTreeNode)
                {
                    Authority authority = new Authority();
                    if (menu != null)
                    {
                        if (menu.state.ContainsKey("isFunction") == false)
                        {
                            authority.Role = roleReps.FindBy(r => r.Id == roleId).First();

                            authority.Menu = menuReps.FindBy(m => m.Id == menu.Id).First();

                            var auth = authorityReps.FindBy(a => a.Menu.Id == menu.Id)
                                .Where(a => a.Role.Id == roleId).FirstOrDefault();

                            if (auth == null)
                            {
                                authority.Id = Guid.NewGuid();

                                authorityReps.Add(authority);

                            }
                        }
                        if (menu.state.ContainsKey("isFunction") == true)
                        {
                            Rela_Authority_Function relaAuthorityFunctions = new Rela_Authority_Function();

                            relaAuthorityFunctions.Authority = roleReps.FindBy(r => r.Id == roleId)
                                .SelectMany(a => a.Authorities).First();

                            var func = authorityReps.FindAll().SelectMany(g => g.Rela_Authority_Function).
                                Where(r => r.Function.Id == menu.Id).FirstOrDefault();
                            if (func == null)
                            {

                                relaAuthorityFunctions.Id = Guid.NewGuid();

                                authority.Rela_Authority_Function.Add(relaAuthorityFunctions);
                            }
                        }
                    }
                }
            }

            //删除数据库存在但用户未选择的权限
            foreach (var authorityItem in unCheckedNode)
            {
                if (authorityItem.state.ContainsKey("isFunction") == false)
                {
                    var author = authorityReps.FindBy(a => a.Menu.Id == authorityItem.Id)
                   .Where(a => a.Role.Id == roleId).FirstOrDefault();
                    if (author != null)
                    {
                        authorityReps.Remove(author);
                    }
                }
            }

            //添加角色下的用户
            if (userNode != null)
            {
                foreach (var user in userNode)
                {
                    if (user != new Guid())
                    {
                        Rela_Role_User rUser = new Rela_Role_User();
                        rUser.Role = roleReps.FindBy(r => r.Id == roleId).FirstOrDefault();
                        rUser.User = userReps.FindBy(u => u.Id == user).FirstOrDefault();

                        var IsExist = roleReps.FindBy(r => r.Id == roleId).SelectMany(r =>
                          r.Rela_Role_User).Where(r => r.User.Id == user).FirstOrDefault();
                        //不存在就添加
                        if (IsExist == null)
                        {
                            rUser.Id = Guid.NewGuid();
                            rUser.Name = rUser.User.Name;
                            rUser.CreatedTime = DateTime.Now;
                            rUser.UpdatedTime = DateTime.Now;
                            rUser.Email = rUser.User.Email;
                            rUser.CreatorId = 1;
                            rUser.UpdaterId = 1;

                            rUser.Role.Rela_Role_User.Add(rUser);
                        }
                    }
                }
            }

            //删除角色下的用户
            foreach (var userId in unCheckUsers)
            {
                if (userId != null) {
                    var user = roleReps.FindBy(r => r.Id == roleId).SelectMany(r => r.Rela_Role_User).
                  Where(r => r.User.Id == userId && r.Role.Id == roleId).FirstOrDefault();
                    if (user != null)
                    {
                        roleReps.DeleteRela_Role_User(user);
                        //role.Rela_Role_User.Remove(user);
                    }
                }
            }
            Commit();
        }

        /// <summary>
        /// 根据角色Id查询对应用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IQueryable<DtoUser> GetUsersByRoleId(Guid roleId)
        {
            var users = roleReps.FindBy(r => r.Id == roleId).SelectMany(r => r.Rela_Role_User).Select(m => m.User);

            var dtoUsers = users.ConvertTo<User, DtoUser>();

            return dtoUsers;
        }
    }
}
