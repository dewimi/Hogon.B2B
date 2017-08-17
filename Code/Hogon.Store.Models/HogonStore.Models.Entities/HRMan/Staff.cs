using Hogon.Framework.Core.UnitOfWork.EntityFramework;
using Hogon.Store.Models.Entities.MemberMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.Entities.HRMan
{
    public class Staff : BaseEntity
    {
        /// <summary>
        /// 企业账号
        /// </summary>
        public Enterprise Enterprise { get; set; }

        /// <summary>
        /// 个人账号
        /// </summary>
        public Person Person { get; set; }
    }
}
