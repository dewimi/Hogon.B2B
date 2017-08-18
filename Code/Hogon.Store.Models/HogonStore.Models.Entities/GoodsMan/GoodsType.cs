using Hogon.Framework.Core.UnitOfWork.EntityFramework;
using System;
using System.Collections.Generic;

namespace Hogon.Store.Models.Entities.GoodsMan
{
    /// <summary>
    /// 商品分类
    /// </summary>
    public class GoodsType : BaseEntity
    {
        public GoodsType()
        {
            Children = new HashSet<GoodsType>();
            Rela_Goods_GoodsType = new HashSet<Rela_Goods_GoodsType>();
            Rela_Brand_GoodsType = new HashSet<Rela_Brand_GoodsType>();
        }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 上级分类
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 父级分类集合
        /// </summary>        
        public virtual GoodsType Parent { get; set; }

        /// <summary>
        /// 子集分类集合
        /// </summary>
        public virtual ICollection<GoodsType> Children { get; set; }
        
        /// <summary>
        /// 分类描述
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        public ProductType ProductType { get; set; }

        /// <summary>
        /// 商品商品分类关联集合
        /// </summary>
        public virtual ICollection<Rela_Goods_GoodsType> Rela_Goods_GoodsType { get; set; }

        /// <summary>
        /// 品牌商品分类关联集合
        /// </summary>
        public virtual ICollection<Rela_Brand_GoodsType> Rela_Brand_GoodsType { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
    }
}
