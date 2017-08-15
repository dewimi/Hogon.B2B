using Hogon.Store.Models.Entities.GoodsMan;
using System.Data.Entity.ModelConfiguration;

namespace Hogon.Store.Models.FluentAPI.GoodsMan
{
    public class AppCaseConfiguration : EntityTypeConfiguration<AppCase>
    {

        public AppCaseConfiguration()
        {
            Property(p => p.Subject).HasMaxLength(20);
            Property(p => p.Author).HasMaxLength(20);
            Property(p => p.AppIndustry).HasMaxLength(20);
            Property(p => p.Usage).HasMaxLength(20);
        }
    }
}
