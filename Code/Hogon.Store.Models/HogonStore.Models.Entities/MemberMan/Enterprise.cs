using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.Entities.MemberMan
{
    /// <summary>
    /// 企业
    /// </summary>
    public class Enterprise :Account
    {
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string ConnectPeopleName { get; set; }

        /// <summary>
        /// 联系人所在部门
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string EnterpriseName { get; set; }

        /// <summary>
        /// 公司所在地址
        /// </summary>
        public string EnterpriseAddress { get; set; }

        /// <summary>
        /// 公司所属行业
        /// </summary>
        public string EnterpriseType { get; set; }
    }
}
