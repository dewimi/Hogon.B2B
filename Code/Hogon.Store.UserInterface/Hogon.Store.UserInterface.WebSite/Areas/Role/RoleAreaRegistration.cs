using System.Web.Mvc;

namespace Hogon.Store.UserInterface.WebSite.Areas.Role
{
    public class RoleAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Role";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Role_default",
                "Role/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}