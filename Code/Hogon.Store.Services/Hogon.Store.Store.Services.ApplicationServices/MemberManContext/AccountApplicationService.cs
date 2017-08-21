using AutoMapper;
using Hogon.Framework.Core.Common;
using Hogon.Framework.Core.Owin;
using Hogon.Framework.Core.UnitOfWork;
using Hogon.Store.Models.Dto.MemberMan;
using Hogon.Store.Models.Dto.Security;
using Hogon.Store.Models.Entities.HRMan;
using Hogon.Store.Models.Entities.MemberMan;
using Hogon.Store.Models.Entities.Security;
using Hogon.Store.Models.Module.Security;
using Hogon.Store.Repositories.MemberMan;
using Hogon.Store.Repositories.Security;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Security;

namespace Hogon.Store.Services.ApplicationServices.MemberManContext
{
    public class AccountApplicationService : BaseApplicationService
    {
        AccountRepository _accountReoisitory = new AccountRepository();
        EnterpriseRepository _enterpriseReoisitory = new EnterpriseRepository();
        PersonRepository _personRepository = new PersonRepository();
        RoleRepository _roleRepository = new RoleRepository();
        Md5Encryptor _encryptor = new Md5Encryptor();
        RoleFactory _roleFactory = new RoleFactory();

        public AccountApplicationService()
        {

        }

        /// <summary>
        /// 根据用户名称查询角色
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public DtoRole GetRoleByAccountId(Guid userId)
        {
            var account = _accountReoisitory.FindBy(r => r.Id == userId)
                .OfType<Person>().First();

            var roleFactory = new RoleFactory();
            var role = account.GetCurrentRole(roleFactory);

            Mapper.Initialize(cfg => cfg.CreateMap<Role, DtoRole>());
            var roleData = Mapper.Map<DtoRole>(role);

            return roleData;
        }

        /// <summary>
        /// 根据用户查询相应权限
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public IQueryable<DtoMenu> GetAuthorityByAccountId(Guid accountId)
        {
            var menus = UserState.Current.AvailableMenus.AsQueryable();

            var dtoMenus = menus.ConvertTo<MenuState, DtoMenu>();

            return dtoMenus;
        }

        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <returns></returns>
        public int IsExist(string userName)
        {
            int result = 0;
            var user = _accountReoisitory.FindBy(i => i.Name == userName).FirstOrDefault();

            if (user == null)
            {
                result = 0;
            }
            else
            {
                result = 1;
            }
            return result;
        }

        /// <summary>
        /// 查询所有用户
        /// </summary>
        /// <returns></returns>
        public IQueryable<DtoAccount> GetAllAccount()
        {
            var accounts = _accountReoisitory.FindAll();

            var dtoAccounts = accounts.ConvertTo<Account, DtoAccount>();

            return dtoAccounts;
        }

        /// <summary>
        /// 根据用户Id查询信息
        /// </summary>
        /// <returns></returns>
        public DtoAccount GetAccountById(Guid Id)
        {
            var user = _accountReoisitory.FindBy(i => i.Id == Id).First();
            Mapper.Initialize(cfg => cfg.CreateMap<Account, DtoAccount>());
            var users = Mapper.Map<DtoAccount>(user);

            return users;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Login(string userName, string password)
        {
            // Check user login credential.
            var currentAccount = Validate(userName, password);

            if (currentAccount == null)
            {
                return false;
            }

            // If input credential is available, set cookie.
            FormsAuthentication.SetAuthCookie(userName, false);

            UserState userState = new UserState()
            {
                AccountId = currentAccount.Id,
                UserName = currentAccount.Name,
                Email = currentAccount.EmailAddress
            };
            
            userState.AvailableMenus = currentAccount
                .GetAvailableMenus(_roleFactory).AsQueryable()
                .ConvertTo<Menu, MenuState>().ToList();
            
            userState.AvailableFunctions = currentAccount
                .GetAvailableFunctions(_roleFactory).Select(m => new FunctionState()
                {
                    Id = m.Id,
                    Name = m.Name,
                    MenuCode = m.Menu.Code,
                    FunctionCode = m.Code,
                    IsEnable = m.IsEnable,
                }).Where(m => m.IsEnable == true).ToList();

            UserState.Current = userState;
            if (currentAccount == null)
            {
                return false;
            }

            // If input credential is available, set cookie.
            FormsAuthentication.SetAuthCookie(userName, false);
            return true;
        }


        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public void Logout()
        {
            FormsAuthentication.SignOut();
        }

        /// <summary>
        /// 保存个人用户信息
        /// </summary>
        public void SaveUser(DtoAccount dtoAccount)
        {
            if (dtoAccount.Id == new Guid())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DtoAccount, Person>());

                // 如果密码是新密码，对密码进行加密
                dtoAccount.Password = _encryptor.Encrypt(dtoAccount.Password);

                var account = Mapper.Map<Person>(dtoAccount);
                account.Id = Guid.NewGuid();
                account.CreateTime = DateTime.Now;
                account.UpdateTime = DateTime.Now;
                account.EmailAddress = dtoAccount.EmailAddress;
                account.PhoneNumber = dtoAccount.PhoneNumber;
                account.Name = dtoAccount.Name;
                account.IsEnable = true;
                account.Password = dtoAccount.Password;
                
                _accountReoisitory.Add(account);
            }

            try
            {
                Commit();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        /// <summary>
        /// 保存企业用户信息
        /// </summary>
        /// <param name="dtoAccount"></param>
        public void SaveCompanyUser(DtoAccount dtoAccount)
        {
            if (dtoAccount.Id == new Guid())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DtoAccount, Enterprise>());

                // 如果密码是新密码，对密码进行加密
                dtoAccount.Password = _encryptor.Encrypt(dtoAccount.Password);

                var account = Mapper.Map<Enterprise>(dtoAccount);
                account.Id = Guid.NewGuid();
                account.CreateTime = DateTime.Now;
                account.UpdateTime = DateTime.Now;
                account.EmailAddress = dtoAccount.EmailAddress;
                account.PhoneNumber = dtoAccount.PhoneNumber;
                account.Name = dtoAccount.Name;
                account.IsEnable = true;
                account.Password = dtoAccount.Password;
                account.ConnectPeopleName = dtoAccount.ConnectPeopleName;
                account.EnterpriseAddress = dtoAccount.EnterpriseAddress;
                account.EnterpriseName = dtoAccount.EnterpriseName;
                account.EnterpriseType = dtoAccount.EnterpriseType;
                _accountReoisitory.Add(account);
            }

            try
            {
                Commit();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        /// <summary>
        /// 用户名密码验证
        /// </summary>s
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Account Validate(string userName, string password)
        {
            password = _encryptor.Encrypt(password);

            // 使用加密后的密码进行比较
            var currentUser = _accountReoisitory.FindAll()
                .Where(u => u.Name == userName && u.Password == password)
                .FirstOrDefault();

            if (currentUser == null)
            {
                currentUser = _accountReoisitory.FindAll().OfType<Person>()
                                .Where(u => u.PhoneNumber == userName && u.Password == password)
                                .FirstOrDefault();
            }

            return currentUser;
        }

        /// <summary>
        /// 检查个人用户名是否被注册过
        /// </summary>
        /// <param name="name">用户名</param>
        /// <returns></returns>
        public Account CheckUserAccount(string name)
        {
            var currentUser = _accountReoisitory.FindAll().OfType<Person>()
                .Where(u => u.Name == name)
                .FirstOrDefault();

            return currentUser;
        }

        /// <summary>
        /// 检查企业用户名是否被注册过
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Account CheckEnterpriseAccount(string name)
        {
            var currentUser = _accountReoisitory.FindAll().OfType<Enterprise>()
            .Where(u => u.Name == name)
            .FirstOrDefault();

            return currentUser;
        }

        /// <summary>
        /// 检查个人手机号是否被注册过
        /// </summary>
        /// <param name="phoneNumebr">手机号</param>
        /// <returns></returns>
        public Account CheckUserPhone(string phoneNumebr)
        {
            var currentUser = _accountReoisitory.FindAll().OfType<Person>()
                .Where(u => u.PhoneNumber == phoneNumebr)
                .FirstOrDefault();

            return currentUser;
        }

        /// <summary>
        /// 检查企业手机号是否被注册
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public Account CheckEnterprisePhone(string phoneNumber)
        {
            var currentUser = _accountReoisitory.FindAll().OfType<Enterprise>()
             .Where(u => u.PhoneNumber == phoneNumber)
             .FirstOrDefault();

            return currentUser;
        }

        /// <summary>
        /// 检查个人邮箱是否被注册过
        /// </summary>
        /// <param name="emailAddress">邮箱</param>
        /// <returns></returns>
        public Account CheckUserEmail(string emailAddress)
        {
            var currentUser = _accountReoisitory.FindAll().OfType<Person>()
                .Where(u => u.EmailAddress == emailAddress)
                .FirstOrDefault();

            return currentUser;
        }

        public Account CheckEnterpriseEmail(string emaillAddress)
        {
            var currentUser = _accountReoisitory.FindAll().OfType<Enterprise>()
                .Where(u => u.EmailAddress == emaillAddress)
                .FirstOrDefault();

            return currentUser;
        }

        /// <summary>
        /// 检查公司名称是否被注册过
        /// </summary>
        /// <param name="companyName">公司名称</param>
        /// <returns></returns>
        /// 
        public Account CheckCompany(string companyName)
        {
            var currentUser = _accountReoisitory.FindAll().OfType<Enterprise>()
               .Where(u => u.EnterpriseName == companyName)
               .FirstOrDefault();

            return currentUser;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public DtoAccount GetUserInfo(string userInfo)
        {

            Account acount = null;
            if (userInfo.Length == 11)
            {
                acount = _accountReoisitory.FindBy(m => m.PhoneNumber == userInfo).OfType<Person>().FirstOrDefault();
            }
            else
            {
                acount = _accountReoisitory.FindBy(m => m.Name == userInfo).OfType<Person>().FirstOrDefault();
            }
            if (acount == null)
            {
                return null;
            }
            else
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Account, DtoAccount>());
                var dtoAccount = Mapper.Map<DtoAccount>(acount);

                return dtoAccount;
            }

        }

        /// <summary>
        /// 判断个人账号是否存在企业中
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsExist(Guid id)
        {
            bool result = false;
            var staff = _enterpriseReoisitory.FindAll().SelectMany(m => m.Staffs)
                .Where(s => s.Id == id).FirstOrDefault();
            if (staff == null)
                result = true;
            else
                result = false;

            return result;         
        }

        /// <summary>
        /// 保存用户信息到企业下
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void SaveUserInfo(Guid id)
        {
            if (id != new Guid())
            {
                Enterprise enterprise = _enterpriseReoisitory.FindBy
                    (m => m.Id == new Guid("b744d6f9-8ec7-4d65-ae5a-05b712e51663")).First();

                Staff staff = new Staff
                {
                    Person = _personRepository.FindBy(m => m.Id == id).First(),
                };

                enterprise.Staffs.Add(staff);
            }

            Commit();
        }

        /// <summary>
        /// 获取企业下的角色
        /// </summary>
        /// <returns></returns>
        public IQueryable<DtoRole> GetRole()
        {
          var roles = _roleRepository.FindBy(m => m.Enterprise.Id 
                == new Guid("b744d6f9-8ec7-4d65-ae5a-05b712e51663"));
           var data = roles.ConvertTo<Role, DtoRole>();

            return data;
        }

        /// <summary>
        /// 添加员工账号
        /// </summary>
        /// <param name="person"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public int AddAccount(DtoPerson dtoPerson, Guid roleId)
        {
           var result = _personRepository.FindBy(m => m.Name == dtoPerson.Name).FirstOrDefault();
           var email = _personRepository.FindBy(m => m.EmailAddress == dtoPerson.EmailAddress).FirstOrDefault();
           var phoneNumber = _personRepository.FindBy(m => m.PhoneNumber == dtoPerson.PhoneNumber).FirstOrDefault();
            if (result != null)
            {
                return 1;
            }
            else if (email != null)
            {
                return 2;
            }
            else if (phoneNumber != null)
            {
                return 3;
            }
            else
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DtoPerson, Person>());
                var person = Mapper.Map<Person>(dtoPerson);
                person.Password = _encryptor.Encrypt("123456");
                person.IsEnable = true;
                _personRepository.Add(person);

                Enterprise enterprise = _enterpriseReoisitory.FindBy
                       (m => m.Id == new Guid("b744d6f9-8ec7-4d65-ae5a-05b712e51663")).First();
                Staff staff = new Staff
                {
                    Person = person,
                    Role = _enterpriseReoisitory.FindAll().SelectMany(m => m.Roles).Where(r => r.Id == roleId).First()
                };
                enterprise.Staffs.Add(staff);
            }

            Commit();
            return 0;
        }
    }
}