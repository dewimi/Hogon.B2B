using Hogon.Framework.Core.UnitOfWork.EntityFramework;
using System;
using System.Collections.Generic;

namespace Hogon.Store.Models.Entities.GoodsMan
{

    /// <summary>
    /// 产品类型分类
    /// </summary>
    public class ProductTypeCategory : BaseEntity
    {
        public ProductTypeCategory()
        {
            Children = new HashSet<ProductTypeCategory>();
        }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string ProductTypeCategoryName { get; set; }

        /// <summary>
        /// 分类编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 所属分类Id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 父级分类集合
        /// </summary>        
        public virtual ProductTypeCategory Parent { get; set; }

        /// <summary>
        /// 子集分类集合
        /// </summary>
        public virtual ICollection<ProductTypeCategory> Children { get; set; }

        /// <summary>
        /// 产品类型集合
        /// </summary>
        public virtual ICollection<ProductType> ProductType { get; set; }
    }
}