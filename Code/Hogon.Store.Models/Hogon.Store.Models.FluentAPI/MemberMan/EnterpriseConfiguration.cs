using Hogon.Store.Models.Entities.MemberMan;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.FluentAPI.MemberMan
{
    public class EnterpriseConfiguration: EntityTypeConfiguration<Enterprise>
    {
        public EnterpriseConfiguration()
        {
            Map(m =>
            {
                m.ToTable("Enterprise");
            });

        }
    }
}
