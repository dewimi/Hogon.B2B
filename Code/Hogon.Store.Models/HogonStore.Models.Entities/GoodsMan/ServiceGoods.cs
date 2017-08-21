using Hogon.Store.Models.Entities.Common;
using System.Collections.Generic;

namespace Hogon.Store.Models.Entities.GoodsMan
{
    /// <summary>
    /// 服务商品
    /// </summary>
    public class ServiceGoods : Goods
    {
        /// <summary>
        /// 产品
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// 子商品集合
        /// </summary>
        public override ICollection<Goods> ChildrenGoods { get; set; }

        /// <summary>
        /// 服务商名称
        /// </summary>
        public string ServicerName { get; set; }

        /// <summary>
        /// 图片文件
        /// </summary>
        public FileUpload FileUpload { get; set; }
    }
}