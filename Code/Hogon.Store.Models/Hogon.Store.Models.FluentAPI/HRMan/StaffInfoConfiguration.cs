using Hogon.Store.Models.Entities.HRMan;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.FluentAPI.HRMan
{
    public class StaffConfiguration: EntityTypeConfiguration<Staff>
    {
        public StaffConfiguration()
        {
            HasRequired(m => m.Enterprise).WithMany(m => m.Staffs)
               .Map(m => m.MapKey("EnterpriseId"));
            HasRequired(m => m.Person).WithMany()
               .Map(m => m.MapKey("PersonId"));
            HasOptional(m => m.Role).WithMany(m => m.Staffs).Map(m => m.MapKey("RoleId"));
        }
    }
}
