using System;
using Hogon.Framework.Utilities.SmartList.Attributes;

namespace Hogon.Store.UserInterface.Admin.Areas.GoodsMan.Models
{
    public class AppCaseViewModel
    {
        /// <summary>
        /// 主题
        /// </summary>
        [Field("主题")]
        public string Subject { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        [Field("作者")]
        public string Author { get; set; }

        /// <summary>
        /// 应用行业
        /// </summary>
        [Field("应用行业")]
        public string AppIndustry { get; set; }

        /// <summary>
        /// 用途
        /// </summary>
        [Field("用途")]
        public string Usage { get; set; }
    }
}
