using Hogon.Store.Models.Entities.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.FluentAPI.Security
{
    public class Rela_Role_UserConfiguration : EntityTypeConfiguration<Rela_Role_Account>
    {
        public Rela_Role_UserConfiguration()
        {
            HasRequired(m => m.Role).WithMany(m => m.Rela_Role_Account).Map(m => m.MapKey("RoleId"));
            HasRequired(m => m.Account).WithMany(m => m.Rela_Role_Account).Map(m => m.MapKey("UserId"));
        }
    }
}
