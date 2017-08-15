using Hogon.Models.Entities.Security;
using System.Data.Entity.ModelConfiguration;

namespace Hogon.Models.FluentAPI.Security
{
    public class GeneralFunctionConfiguration: EntityTypeConfiguration<GeneralFunction>
    {
        public GeneralFunctionConfiguration()
        {
            Map(t => t.MapInheritedProperties());
            ToTable("GeneralFunction");
        }
    }
}