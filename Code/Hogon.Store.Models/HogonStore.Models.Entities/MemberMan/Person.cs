using Hogon.Framework.Core.Common;
using Hogon.Store.Models.Entities.HRMan;
using Hogon.Store.Models.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;

namespace Hogon.Store.Models.Entities.MemberMan
{
    /// <summary>
    /// 个人
    /// </summary>
    public class Person : Account
    {
        public Person()
        {
            Staffs = new HashSet<Staff>();
        }

        /// <summary>
        /// 员工信息
        /// </summary>
        public ICollection<Staff> Staffs { get; set; }

        /// <summary>
        /// 获取当前员工身份信息
        /// </summary>
        public Staff GetCurrentStaff()
        {
            Staff currentStaff = null;

            if (CurrentIdentity.GetType() == typeof(Enterprise))
                currentStaff = Staffs.Where(m => m.Enterprise == CurrentIdentity).First();

            return currentStaff;
        }

        public override Account CurrentIdentity
        {
            get
            {
                if (base.CurrentIdentity == null)
                {
                    return this;
                }
                else
                {
                    foreach (var staff in Staffs)
                    {
                        if (staff.Enterprise.Id == base.CurrentIdentity.Id)
                            return staff.Enterprise;
                    }

                    base.CurrentIdentity = null;
                    FormsAuthentication.SignOut();

                    throw new UserFriendlyException("当前用户身份不可用，请重新登录.");
                }

            }

            set
            {
                if (value.GetType() != typeof(Enterprise))
                    throw new InvalidOperationException("");

                base.CurrentIdentity = value;
            }
        }
    }
}
