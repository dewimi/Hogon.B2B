using System.Web.Mvc;

namespace Hogon.Store.UserInterface.Admin.Areas.GoodsMan
{
    public class GoodsManAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "GoodsMan";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "GoodsMan_default",
                "GoodsMan/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}