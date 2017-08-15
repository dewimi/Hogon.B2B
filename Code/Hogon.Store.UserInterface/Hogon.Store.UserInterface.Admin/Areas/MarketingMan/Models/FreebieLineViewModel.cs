using Hogon.Framework.Utilities.SmartList.Attributes;
using Hogon.Store.Models.Entities.GoodsMan;
using Hogon.Store.Models.Entities.MarketingMan;
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

        /// <summary> 
        /// 赠品
        /// </summary>
        public Freebie Freebie { get; set; }

        /// <summary>
        /// 产品商品
        /// </summary>
        public ProductGoods ProductGoods { get; set; }

        private string freebieName;
        [TextSearch("分类名称")]
        [Field("赠品名称")]
        /// <summary>
        /// 赠品名称
        /// </summary>
        public string FreebieName
        {
            get {return freebieName; }
            set { freebieName = ProductGoods.Product.ProductName; }
        }

        private string freebieCatalogName;
        [Field("分类")]
        /// <summary>
        /// 赠品所属分类
        /// </summary>
        public string FreebieCaltalogName
        {
            get { return freebieCatalogName; }
            set { freebieCatalogName = Freebie.FreebieCatalog.FreebieCatalogName; }
        }
    }
}