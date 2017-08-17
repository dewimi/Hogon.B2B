using Hogon.Store.Models.Dto.Common;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Hogon.Store.Models.Dto.GoodsMan
{
	/// <summary>
	/// Brand
	/// </summary>
	public class DtoBrand : BaseDto
    {
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
        /// 产品id
        /// </summary>
        public IEnumerable<Guid> ProductIds { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public IEnumerable<string> ProductNames { get; set; }

        /// <summary>
        /// 商品分类Id
        /// </summary>
        public IEnumerable<Guid> GoodsTypeIds { get; set; }

        /// <summary>
        /// 商品分类名称
        /// </summary>
        public IEnumerable<string> GoodsTypeNames { get; set; }

        /// <summary>
        /// 产品集合
        /// </summary>
        public virtual ICollection<DtoProduct> Product { get; set; }

        /// <summary>
        /// 品牌商品分类关联集合
        /// </summary>
        public virtual ICollection<DtoRela_Brand_GoodsType> Rela_Brand_GoodsType { get; set; }

        /// <summary>
        /// 文件id
        /// </summary>
        public Guid FileUploadId { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileUploadName { get; set; }

        /// <summary>
        /// 图片文件
        /// </summary>
        public DtoFileUpload FileUpload { get; set; }
    }
}
