using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.Dto.Security
{
    public class DtoFunction
    {
        /// <summary>
        /// 功能Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 功能名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 功能编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 功能是否可用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 功能创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 功能修改时间
        /// </summary>
        public DateTime UpdatedTime { get; set; }

        /// <summary>
        /// 功能创建人Id
        /// </summary>
        public int CreatorId { get; set; }

        /// <summary>
        /// 功能修改人Id
        /// </summary>
        public int UpdaterId { get; set; }
    }
}
