using Hogon.Store.Models.Entities.GoodsMan;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.FluentAPI.GoodsMan
{
    public class SpecParameterTemplateConfiguration: EntityTypeConfiguration<SpecParameterTemplate>
    {
        public SpecParameterTemplateConfiguration()
        {
            Property(e => e.ParameterName).HasMaxLength(50);
        
            HasOptional(m => m.FileUpload).WithMany(m => m.SpecParameterTemplate).Map(m => m.MapKey("FileId"));
        }
    }
}
