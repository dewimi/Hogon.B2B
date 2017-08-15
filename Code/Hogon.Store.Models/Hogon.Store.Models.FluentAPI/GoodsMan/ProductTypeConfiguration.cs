using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hogon.Store.Models.Entities.GoodsMan;
using Hogon.Framework.Core.UnitOfWork.EntityFramework;

namespace Hogon.Store.Models.FluentAPI.GoodsMan
{
    public class ProductTypeConfiguration : EntityTypeConfiguration<ProductType>
    {
        public ProductTypeConfiguration()
        {
            Property(e => e.TypeName).HasMaxLength(50);
            Property(e => e.CreatePerson).HasMaxLength(50);
            Property(e => e.UpdatePerson).HasMaxLength(50);
            

            HasRequired(m => m.ProductTypeCategory).WithMany(m => m.ProductType).Map(m => m.MapKey("CategoryId"));
        }
    }
}
