using AutoMapper;
using Hogon.Framework.Core.Common;
using Hogon.Framework.Core.Owin;
using Hogon.Framework.Core.UnitOfWork;
using Hogon.Store.Models.Dto.MemberMan;
using Hogon.Store.Models.Dto.Security;
using Hogon.Store.Models.Entities.MemberMan;
using Hogon.Store.Models.Entities.Security;
using Hogon.Store.Repositories.MemberMan;
using Hogon.Store.Services.DomainServices.SecurityContext;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Hogon.Store.Services.ApplicationServices.MemberManContext
{
    public class AccountApplicationService : BaseApplicationService
    {
        AccountRepository accountRepository = new AccountRepository();
        AuthorizationDomainService _authorizationService = new AuthorizationDomainService();
        Md5Encryptor _encryptor = new Md5Encryptor();

        public AccountApplicationService()
        {

        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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
        //        UserName = userName,
        //    };
        //    UserState.Current = userState;
        //    return true;
        //}

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

                //account.Responsor = new Person()
                //{
                //    Id = Guid.NewGuid(),
                //    EmailAddress = userInfo.EmailAddress,
                //    IsEnable = true,
                //    PersonName = userInfo.PersonName,
                //    PhoneNumber = userInfo.PhoneNumber
                //};
                accountRepository.Add(account);
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
                accountRepository.Add(account);
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
        public Account ValidateUser(string userName, string password)
        {
            password = _encryptor.Encrypt(password);

            // 使用加密后的密码进行比较  
            //var currentUser = accountRepository.Include("Account")
            var currentUser = accountRepository.FindAll().OfType<Person>()
                .Where(u => u.Name == userName && u.Password == password)
                .FirstOrDefault();

            if (currentUser == null)
            {
                currentUser = accountRepository.FindAll().OfType<Person>()
                                .Where(u => u.PhoneNumber == userName && u.Password == password)
                                .FirstOrDefault();
            }

            if (currentUser == null)
            {
                currentUser = accountRepository.FindAll().OfType<Person>()
                .Where(u => u.EmailAddress == userName && u.Password == password)
                .FirstOrDefault();
            }
            return currentUser;
        }

        public Account ValidateEnterprise(string userName, string password)
        {
            password = _encryptor.Encrypt(password);
            var currentUser = accountRepository.FindAll().OfType<Enterprise>()
                .Where(u => u.Name == userName && u.Password == password)
                .FirstOrDefault();

            if(currentUser==null)
            { 
            currentUser = accountRepository.FindAll().OfType<Enterprise>()
                .Where(u => u.EmailAddress == userName && u.Password == password)
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
            var currentUser = accountRepository.FindAll().OfType<Person>()
                .Where(u => u.Name == name )
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
            var currentUser = accountRepository.FindAll().OfType<Enterprise>()
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
            var currentUser = accountRepository.FindAll().OfType<Person>()
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
            var currentUser = accountRepository.FindAll().OfType<Enterprise>()
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
            var currentUser = accountRepository.FindAll().OfType<Person>()
                .Where(u => u.EmailAddress == emailAddress)
                .FirstOrDefault();

            return currentUser;
        }

        public Account CheckEnterpriseEmail(string emaillAddress)
        {
            var currentUser = accountRepository.FindAll().OfType<Enterprise>()
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
            var currentUser = accountRepository.FindAll().OfType<Enterprise>()
               .Where(u => u.EnterpriseName == companyName)
               .FirstOrDefault();

            return currentUser;
        }
    }
}
