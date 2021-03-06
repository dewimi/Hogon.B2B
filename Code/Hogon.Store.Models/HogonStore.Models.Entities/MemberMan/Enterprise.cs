﻿using Hogon.Store.Models.Entities.HRMan;
using Hogon.Store.Models.Entities.Security;
using System;
using System.Collections.Generic;

namespace Hogon.Store.Models.Entities.MemberMan
{
    /// <summary>
    /// 企业
    /// </summary>
    public class Enterprise : Account
    {
        public Enterprise()
        {
            Staffs = new HashSet<Staff>();
            Roles = new HashSet<Role>();
        }

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

        /// <summary>
        /// 员工信息
        /// </summary>
        public ICollection<Staff> Staffs { get; set; }

        /// <summary>
        /// 角色集合
        /// </summary>
        public ICollection<Role> Roles { get; set; }

        public override Account CurrentIdentity
        {
            get
            {
                return this;
            }
            set
            {
                throw new InvalidOperationException("企业账户不可以设置当前身份");
            }
        }
    }
}