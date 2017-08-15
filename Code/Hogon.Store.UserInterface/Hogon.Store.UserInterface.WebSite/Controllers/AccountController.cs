using Hogon.Framework.Core.Common;
using Hogon.Store.Models.Dto.MemberMan;
using Hogon.Store.Services.ApplicationServices.MemberManContext;
using Hogon.Store.Services.ApplicationServices.SecurityContext;
using System.Security.Principal;
using System.Web.Mvc;

namespace Hogon.Store.UserInterface.WebSite.Controllers
{
    public class AccountController : Controller
    {
        //UserApplicationService _userService;
        AccountApplicationService _accountService ;

        public AccountController()
        {
            //_userService = new UserApplicationService();
            _accountService = new AccountApplicationService();
        }

        public bool CheckAccount(string userName,string password)
        {
            if (userName.Trim().Length < 4 || userName.Trim().Length>16 || password.Trim().Length<4 || password.Trim().Length>16)
            {
                return false;
            }
            return true;
        }

        public ActionResult Login()
        {
            return View();
        }


        //[HttpPost]
        //public ActionResult Login(LoginModel model)
        //{
        //    var userAccount = _accountService.ValidateUser(model.UserName, model.Password);
        //    var enterPriseAccount = _accountService.ValidateEnterprise(model.UserName, model.Password);

        //    if (userAccount == null && enterPriseAccount == null)
        //    {
        //        //ViewBag.Msg = "用户名或密码错误！";
        //        return Login();
        //    }
        //    return Redirect("/Home/Index");
        //}

        /// <summary>
        /// 登录时判断用户名/密码是否正确
        /// </summary>
        /// <param name = "username" > 用户名 </ param >
        /// < param name="password">密码</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AccountIsExit(string username, string password)
        {
            string message = "";
            var userAccount = _accountService.ValidateUser(username, password);
            var enterPriseAccount = _accountService.ValidateEnterprise(username, password);

            if (userAccount == null && enterPriseAccount == null)
            {
                message = "用户名或密码错误！";
            }

            return Json(message);
        }

        //Get
        public ActionResult UserRegister()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult UserRegister(DtoAccount personAccount)
        {
            _accountService.SaveUser(personAccount);
            return Redirect("/Account/Login");
        }

       //get
        public ActionResult CompanyRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CompanyRegister(DtoAccount companyAccount)
        {
            _accountService.SaveCompanyUser(companyAccount);
            return Redirect("/Account/Login");
        }


        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="name">用户名</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NameIsExit(string userName)
        {
            string message = "";
            var useraAccount = _accountService.CheckUserAccount(userName);
            var enterpriseAccount = _accountService.CheckEnterpriseAccount(userName);
            if (useraAccount != null || enterpriseAccount!=null)
                message = "此用户名已被占用,请重新输入！";
            
            return Json(message);
        }

        /// <summary>
        /// 检查个人注册手机号是否被占用
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PhoneNumberIsExit(string phone)
       {
            string message = "";
            var userAccount = _accountService.CheckUserPhone(phone);
            //var enterpriseAccount = _accountService.CheckEnterprisePhone(phone);
            if (userAccount != null )
                message = "此手机号已被占用,请重新输入！";
            return Json(message);
        }

        /// <summary>
        /// 检查注册邮箱是否被占用
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EmaillIsExit(string email)
        {
            string message = "";
            var userAccount = _accountService.CheckUserEmail(email);
            var enterpriseAccount = _accountService.CheckEnterpriseEmail(email);
            if (userAccount != null  || enterpriseAccount!=null)
                message = "此邮箱已被占用,请重新输入！";
            return Json(message);
        }

        /// <summary>
        /// 检查公司名称是否被占用
        /// </summary>
        /// <param name="companyName">公司名称</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CompanyIsExit(string companyName)
        {
            string message = "";
            var account = _accountService.CheckCompany(companyName);
            if (account != null)
                message = "此公司名已被占用,请重新输入！";

            return Json(message);
        }

        public ActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            _accountService.Logout();

            return Redirect("/Account/Login");
        }
    }

    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}