using Hogon.Framework.Core.Common;
using Hogon.Framework.Core.UnitOfWork;
using Hogon.Framework.Utilities.SmartList;
using Hogon.Store.Models.FluentAPI;
using Microsoft.Practices.Unity;

namespace Hogon.Store.Web.Extension
{
    public static class Bootstrapper
    {
        /// <summary>
        /// Boot when application start
        /// </summary>
        public static void Boot()
        {
            RegisterTypes();
        }

        /// <summary>
        /// >Registers the type mappings with the Unity container.
        /// </summary>
        public static void RegisterTypes()
        {
            var container = IocManager.Container;

            container.RegisterType<IUnitOfWork, HogonContext>(new PerRequestLifetimeManager());
            container.RegisterType(typeof(BaseViewRender<>), typeof(ListRender<>)
                , new PerRequestLifetimeManager());
        }
    }
}