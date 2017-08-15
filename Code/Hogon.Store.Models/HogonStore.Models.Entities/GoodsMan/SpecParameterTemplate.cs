using Hogon.Framework.Core.UnitOfWork.EntityFramework;
using Hogon.Store.Models.Entities.Common;
using System.Collections.Generic;

namespace Hogon.Store.Models.Entities.GoodsMan
{
    /// <summary>
    /// 规格参数模板
    /// </summary>
    public class SpecParameterTemplate: BaseEntity
    {
        public SpecParameterTemplate()
        {
         
        }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// 图片文件
        /// </summary>
        public FileUpload  FileUpload { get; set; }

        /// <summary>
        /// 规格类型
        /// </summary>
        public SpecType SpecType { get; set; }
    }
}
