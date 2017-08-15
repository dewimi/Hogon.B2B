using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.Dto.MemberMan
{
     public class DtoAccount
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 联系人邮箱
        /// </summary>
        public string EmailAddress { get; set; }

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
