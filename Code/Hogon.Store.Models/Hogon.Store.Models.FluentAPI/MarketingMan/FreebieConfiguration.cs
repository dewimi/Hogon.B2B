using Hogon.Store.Models.Entities.MarketingMan;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.FluentAPI.MarketingMan
{
    public class FreebieConfiguration: EntityTypeConfiguration<Freebie>
    {
        public FreebieConfiguration()
        {
            HasRequired(m => m.FreebieCatalog).WithMany().Map(m => m.MapKey("FreebieCatalogId"));
            HasRequired(m => m.Product).WithMany().Map(m => m.MapKey("ProductId"));
        }
    }
}
