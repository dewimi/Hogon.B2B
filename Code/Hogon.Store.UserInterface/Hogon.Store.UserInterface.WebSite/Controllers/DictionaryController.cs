using Hogon.Framework.Web.Controller;
using Hogon.Store.Utilities.Constants.Security;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Hogon.Store.UserInterface.WebSite.Controllers
{
    public class DictionaryController : BaseController
    {
        /// <summary>
        /// 获取品牌数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult FindBrands()
        {
            List<KeyValuePair<int, string>> options = new List<KeyValuePair<int, string>>();

            options.Add(new KeyValuePair<int, string>(1, "禾工"));
            options.Add(new KeyValuePair<int, string>(2, "岛津"));
            options.Add(new KeyValuePair<int, string>(3, "安捷伦"));

            return Json(options);
        }

        /// <summary>
        /// 获取通用功能类型数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult FindFunctionTypes()
        {
            var funcTypes = FunctionType.All;

            var options = new List<KeyValuePair<string, string>>();

            foreach(var funcType in funcTypes)
            {
                options.Add(new KeyValuePair<string, string>(funcType.Code, funcType.TypeName));
            }

            return Json(options);
        }
    }
}