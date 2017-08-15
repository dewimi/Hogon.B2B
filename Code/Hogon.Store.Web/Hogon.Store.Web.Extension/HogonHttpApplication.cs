using System.Web;

namespace Hogon.Store.Web.Extension
{
    public class HogonHttpApplication : HttpApplication
    {
        protected virtual void Application_Start()
        {
            Bootstrapper.Boot();
        }
    }
}