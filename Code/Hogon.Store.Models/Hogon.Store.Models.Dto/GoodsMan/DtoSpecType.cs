using System;
using System.Collections.Generic;

namespace Hogon.Store.Models.Dto.GoodsMan
{
    /// <summary>
    /// 规格类型
    /// </summary>
    public class DtoSpecType : BaseDto
    {
        public IEnumerable<Guid> Ids { get; set; }

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
        public string SpecRemark { get; set; }

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
        public string DisplayMode { get; set; }

        /// <summary>
        /// 产品类型Id
        /// </summary>
        public Guid ProductTypeId { get; set; }

        /// <summary>
        /// 规格参数模板Id
        /// </summary>
        public Guid SpecTypeParameterId { get; set; }

        /// <summary>
        /// 规格参数模板名称
        /// </summary>
        public string SpecTypeParameterName { get; set; }

        /// <summary>
        /// 规格参数模板集合
        /// </summary>
        public IEnumerable<DtoSpecTypeParameter> SpecTypeParameter { get; set; }
    }
}
