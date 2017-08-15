using Hogon.Framework.Core.UnitOfWork.EntityFramework;
using System;
using System.Collections.Generic;

namespace Hogon.Store.Models.Entities.GoodsMan
{
    /// <summary>
    /// 产品类型
    /// </summary>
    public class ProductType : BaseEntity
    {
        public ProductType()
        {
            SpecTypes = new HashSet<SpecType>();
        }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 规格类型集合
        /// </summary>
        public virtual ICollection<SpecType> SpecTypes { get; set; }

        /// <summary>
        /// 产品类型分类
        /// </summary>
        public ProductTypeCategory ProductTypeCategory { get; set; }

        /// <summary>
        /// 商品类型
        /// </summary>
        public virtual ICollection<GoodsType> GoodsClass { get; set; }
    }
}
