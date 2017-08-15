using System.Data.Entity.ModelConfiguration;
using Hogon.Store.Models.Entities.CommonContext;

namespace Hogon.Store.Models.FluentAPI.CommonContext
{
    public class InformationConfiguration: EntityTypeConfiguration<Information>
    {
        public InformationConfiguration()
        {
            Property(e => e.Pname).HasMaxLength(20);
            Property(e => e.Ename).HasMaxLength(20);
            Property(e => e.Brand).HasMaxLength(20);
            Property(e => e.ProductModel).HasMaxLength(20);
            Property(e => e.ProducingArea).HasMaxLength(20);
            Property(e => e.Quote);
            Property(e => e.UploadName).HasMaxLength(500);
            Property(e => e.OriginalFileName).HasMaxLength(200);
        }
    }
}
