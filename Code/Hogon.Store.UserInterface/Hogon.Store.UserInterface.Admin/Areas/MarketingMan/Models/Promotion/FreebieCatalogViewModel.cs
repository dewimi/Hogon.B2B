using Hogon.Framework.Utilities.SmartList.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hogon.Store.UserInterface.Admin.Areas.MarketingMan.Models.Promotion
{
    public class FreebieCatalogViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        [TextSearch("分类名称")]
        [Field("赠品分类名称")]
        /// <summary>
        /// 赠品分类名称
        /// </summary>
        public string FreebieCatalogName { get; set; }

        [Field("排序")]
        /// <summary>
        /// 赠品分类排序
        /// </summary>
        public int Sort { set; get; }
    }
}