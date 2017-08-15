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
    public class ProductTypeCategoryConfiguration : EntityTypeConfiguration<ProductTypeCategory>
    {
        public ProductTypeCategoryConfiguration()
        {
            Property(p => p.ProductTypeCategoryName).HasMaxLength(20).IsRequired().HasUniqueIndexAnnotation("IndexTypeCategoryName", 1);
            Property(p => p.CreatePerson).HasMaxLength(20);
            Property(p => p.UpdatePerson).HasMaxLength(20);
            Property(p => p.Code).HasMaxLength(20);

            HasMany(m => m.Children).WithOptional(m => m.Parent)
                .HasForeignKey(m => m.ParentId);

            
        }
    }
}
