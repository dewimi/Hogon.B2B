using Hogon.Store.Models.Entities.Common;
using Hogon.Store.Models.Entities.GoodsMan;
using System;
using System.Collections.Generic;

namespace Hogon.Store.Models.Dto.GoodsMan
{
	/// <summary>
	/// 服务商品
	/// </summary>
	public class DtoServiceGoods : BaseDto
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        public string GoodsCode { get; set; }

        /// <summary>
        /// 商品描述
        /// </summary>
        public string GoodsDesription { get; set; }

        /// <summary>
        /// 商品别名
        /// </summary>
        public string GoodsAlias { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SalePrice { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsAvailable { get; set; }


        /// <summary>
        /// 商品分类
        /// </summary>
        public IEnumerable<GoodsType> GoodsTypes { get; set; }

        /// <summary>
        /// 商品分类Id
        /// </summary>
        public IEnumerable<Guid> GoodsTypeId { get; set; }

        /// <summary>
        /// 商品分类名称
        /// </summary>
        public IEnumerable<string> GoodsTypeNames { get; set; }

        /// <summary>
        /// 图片Id
        /// </summary>
        public Guid FIleUploadId { get; set; }

        /// <summary>
        /// 图片名字
        /// </summary>
        public string FileUploadName { get; set; }

        /// <summary>
        /// 图片文件
        /// </summary>
        public FileUpload FileUpload { get; set; }
    }
}