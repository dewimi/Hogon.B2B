using System.Linq;
using System.Web.Mvc;
using Hogon.Store.Models.Dto.Security;
using Hogon.Store.UserInterface.WebSite.Models.Home;
using Hogon.Framework.Web.Controller;
using Hogon.Framework.Core.Common;
using Hogon.Framework.Utilities.SmartList;
using Hogon.Store.Services.ApplicationServices.SecurityContext;

namespace Hogon.Store.UserInterface.WebSite.Controllers
{
    public class HomeController : Controller
    {
        MenuApplicationService menuSvc = new MenuApplicationService();

        public HomeController()
        {

        }

        public ActionResult Index()
        {
            return View();
        }
    }
}