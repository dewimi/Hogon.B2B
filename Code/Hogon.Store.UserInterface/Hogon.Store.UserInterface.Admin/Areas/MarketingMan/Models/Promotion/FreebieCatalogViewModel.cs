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
<<<<<<< HEAD
        [Field("赠品名称")]
        /// <summary>
        /// 赠品名称
        /// </summary>
        public string FreebieName { get; set; }

        [Field("分类")]
        /// <summary>
        /// 赠品所属分类
        /// </summary>
        public string FreebieCaltalog { set; get; }
=======
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
>>>>>>> f80cd5d8ef8824bfbf25d550271c21c819442cf4
    }
}