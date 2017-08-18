using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.Dto.HRMan
{
    public class DtoStaff : BaseDto
    {
        /// <summary>
        /// 企业Id
        /// </summary>
        public Guid EnterpriseId { get; set; }

        /// <summary>
        /// 个人Id
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public Guid Role { get; set; }
    }
}
