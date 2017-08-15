using Hogon.Store.Models.Entities.MemberMan;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.FluentAPI.MemberMan
{
    public class AccountConfiguration: EntityTypeConfiguration<Account>
    {
        public AccountConfiguration()
        {
            Property(p => p.Name).HasMaxLength(50);
            Property(p => p.Password).HasMaxLength(50);


        }
    }
}
