using Hogon.Framework.Core.UnitOfWork;

namespace Hogon.Store.Services.ApplicationServices.SecurityContext
{
    public class MenuFunctionApplicationService : BaseApplicationService
    {
     

        public MenuFunctionApplicationService()
        {
         
        }
        /// <summary>
        /// 获取菜单功能
        /// </summary>
        /// <returns></returns>
        //public IQueryable<DtoMenuFunction> GetMenuFunctions()
        //{
        //    var menuFunctions = menufunctionReps.FindAll();
        //    var dtoMenuFunctions = menuFunctions.Select(m => new DtoMenuFunction
        //    {
        //        Id = m.Id,
        //        Code = m.Code,
        //        Name = m.Name,
        //        IsEnable = m.IsEnable,
        //        CreatedTime = m.CreatedTime,
        //        UpdatedTime = m.UpdatedTime,
        //        CreatorId = m.CreatorId,
        //        UpdaterId = m.UpdaterId,
        //    });
        //    //Menu menu = new Menu();

        //    //var newDtoMenus = menuFunctions.SkipWhile(m => menu.MenuFunctions
        //    //.Select(f => f.Id).Contains(m.Id));

        //    //var deletedDtoMenus = menu.MenuFunctions.SkipWhile(m => menuFunctions
        //    //.Select(f => f.Id).Contains(m.Id));

        //    //var dtoMenuFunction = menufunction.MenuFunction<DtoMenuFunction>();

        //    return dtoMenuFunctions;
        //}
    }
}