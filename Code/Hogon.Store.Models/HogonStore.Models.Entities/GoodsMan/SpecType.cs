﻿using Hogon.Framework.Core.UnitOfWork.EntityFramework;
using System.Collections.Generic;

namespace Hogon.Store.Models.Entities.GoodsMan
{
    /// <summary>
    /// 规格类型
    /// </summary>
    public class SpecType: BaseEntity
    {
        public SpecType()
        {
            SpecParameterTemplate = new HashSet<SpecParameterTemplate>();
        }

        /// <summary>
        /// 规格名称
        /// </summary>
        public string SpecName { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        public string SpecCatalog { get; set; }

        /// <summary>
        /// 规格备注
        /// </summary>
        public string SpecRemark{ get; set; }

        /// <summary>
        /// 规格别名
        /// </summary>
        public string SpecSecondName { get; set; }

        /// <summary>
        /// 显示类型
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 显示方式
        /// </summary>
        public string DisplayMode{ get; set; }

        /// <summary>
        /// 规格参数模板集合
        /// </summary>
        public virtual ICollection<SpecParameterTemplate> SpecParameterTemplate { get; set; }
        
        /// <summary>
        /// 产品类型
        /// </summary>
        public ProductType ProductType { get; set; }
    }
}
