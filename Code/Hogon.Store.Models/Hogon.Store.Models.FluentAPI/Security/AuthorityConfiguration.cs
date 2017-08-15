
using Hogon.Store.Models.Entities.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.FluentAPI.Security
{
    public class AuthorityConfiguration : EntityTypeConfiguration<Authority>
    {
        public AuthorityConfiguration()
        {
            HasRequired(m => m.Menu).WithMany(m => m.Authority).Map(m => m.MapKey("MenuId"));
            HasRequired(m => m.Role).WithMany(m => m.Authorities).Map(m => m.MapKey("RoleId"));
        }
    }
}
