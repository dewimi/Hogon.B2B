using System.Web.Mvc;

namespace Hogon.Store.UserInterface.Admin.Areas.MarketingMan
{
    public class MarketingManAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MarketingMan";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MarketingMan_default",
                "MarketingMan/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}