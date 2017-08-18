using Hogon.Store.Models.Entities.HRMan;
using Hogon.Store.Models.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    foreach(var staff in Staffs)
                    {
                        if (staff.Enterprise.Id == base.CurrentIdentity.Id)
                            return staff.Enterprise;
                    }

                    throw new NotImplementedException();
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
