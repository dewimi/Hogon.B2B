using Hogon.Store.Models.Entities.GoodsMan;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.FluentAPI.GoodsMan
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {

        public ProductConfiguration()
        {
            Property(p => p.ProductName).HasMaxLength(20);
            Property(p => p.ProductCode).HasMaxLength(20);
            Property(p => p.SearchPrimaryKey).HasMaxLength(20);
            Property(p => p.ProductDescription).HasMaxLength(20);

            HasRequired(m => m.Brand).WithMany(m => m.Product).Map(m => m.MapKey("BrandId"));
        }
    }
}
