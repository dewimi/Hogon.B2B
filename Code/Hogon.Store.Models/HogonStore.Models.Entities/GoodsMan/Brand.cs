using Hogon.Framework.Core.UnitOfWork.EntityFramework;
using Hogon.Store.Models.Entities.Common;
using System.Collections.Generic;

namespace Hogon.Store.Models.Entities.GoodsMan
{
    /// <summary>
    /// 品牌
    /// </summary>
    public class Brand : BaseEntity
    {
        public Brand()
        {
            Rela_Brand_GoodsType = new HashSet<Rela_Brand_GoodsType>();
        }
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// 品牌Logo
        /// </summary>
        public string BrandLogo { get; set; }

        /// <summary>
        /// 品牌别名
        /// </summary>
        public string BrandAlias { get; set; }

        /// <summary>
        /// 网址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        public string Nation { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 产品集合
        /// </summary>
        public virtual ICollection<Product> Product { get; set; }

        /// <summary>
        /// 品牌商品分类关联集合
        /// </summary>
        public virtual ICollection<Rela_Brand_GoodsType> Rela_Brand_GoodsType { get; set; }

        /// <summary>
        /// 图片文件
        /// </summary>
        public FileUpload FileUpload { get; set; }
    }
}
