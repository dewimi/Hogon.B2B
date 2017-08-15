using Hogon.Framework.Core.UnitOfWork.EntityFramework;
using Hogon.Store.Models.Entities.Common;
using System;

namespace Hogon.Store.Models.Entities.GoodsMan
{
    /// <summary>
    /// 使用说明
    /// </summary>
    public class Instruction : BaseEntity
    {
        /// <summary>
        /// 主题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 应用行业
        /// </summary>
        public string AppIndustry { get; set; }

        /// <summary>
        /// 用途
        /// </summary>
        public string Usage { get; set; }

        /// <summary>
        /// 产品
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// 图片文件
        /// </summary>
        public FileUpload FileUpload { get; set; }
    }
}
