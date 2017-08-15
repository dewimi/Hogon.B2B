using System;

namespace Hogon.Store.Models.Dto.Common
{
    public class DtoFileUpload:BaseDto
    {

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
    }
}
