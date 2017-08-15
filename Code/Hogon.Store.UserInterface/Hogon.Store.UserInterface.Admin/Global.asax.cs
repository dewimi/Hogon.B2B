using Hogon.Store.Web.Extension;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Hogon.Store.UserInterface.Admin
{
    public class MvcApplication : HogonHttpApplication
    {
        protected override void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            base.Application_Start();
        }
    }
}