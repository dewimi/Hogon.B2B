using Hogon.Framework.Utilities.SmartList.Attributes;
using Hogon.Store.Models.Entities.GoodsMan;
using Hogon.Store.Utilities.Constants.GoodsMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hogon.Store.UserInterface.Admin.Areas.MarketingMan.Models
{
    public class FreebieLineViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        [Field("赠品名称")]
        /// <summary>
        /// 赠品名称
        /// </summary>
        public string ProductName { get; set; }

        [Field("分类")]
        /// <summary>
        /// 产品编码
        /// </summary>
        public string ProductCode { get; set; }

    }
}