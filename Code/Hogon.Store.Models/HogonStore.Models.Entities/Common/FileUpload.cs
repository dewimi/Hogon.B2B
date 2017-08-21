using Hogon.Framework.Core.UnitOfWork.EntityFramework;
using Hogon.Store.Models.Entities.GoodsMan;
using System.Collections.Generic;

namespace Hogon.Store.Models.Entities.Common
{
    public class FileUpload : BaseEntity
    {

        public FileUpload()
        {
            SpecParameterTemplate = new HashSet<SpecParameterTemplate>();
        }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public string FileSize { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string Url { get; set; }



        /// <summary>
        /// 规格参数模板集合
        /// </summary>
        public virtual ICollection<SpecParameterTemplate> SpecParameterTemplate { get; set; }
    }
}
