using Hogon.Framework.Core.Common;
using Hogon.Store.Services.ApplicationServices.SecurityContext;
using System.Web.Mvc;

namespace Hogon.Store.UserInterface.Admin.Controllers
{
    public class AccountController : Controller
    {
        UserApplicationService _userService;

        public AccountController()
        {
            _userService = new UserApplicationService();
        }

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (!_userService.Login(model.UserName, model.Password))
                throw new UserFriendlyException("用户名或密码不正确.");
             
            return Redirect("/Home/Index");
        }

        [HttpPost]
        public ActionResult Logout()
        {
            _userService.Logout();

            return Redirect("/Account/Login");
        }
    }

    public class LoginModel
    {
       
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}