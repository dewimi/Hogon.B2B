using Hogon.Store.Models.Entities.GoodsMan;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.FluentAPI.GoodsMan
{
    public class SpecTypeConfiguration: EntityTypeConfiguration<SpecType>
    {
        public SpecTypeConfiguration()
        {
            Property(e => e.SpecName).HasMaxLength(50);
            Property(e => e.SpecCatalog).HasMaxLength(20);
            Property(e => e.SpecRemark).HasMaxLength(200);
            Property(e => e.SpecSecondName).HasMaxLength(50);
            Property(e => e.DisplayName).HasMaxLength(50);
            Property(e => e.DisplayMode).HasMaxLength(50);
            Property(e => e.CreatePerson).HasMaxLength(50);
            Property(e => e.UpdatePerson).HasMaxLength(50);

            HasRequired(m => m.ProductType).WithMany(m => m.SpecTypes).Map(m => m.MapKey("ProductTypeId"));
        }
    }
}
