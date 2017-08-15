using Hogon.Store.Models.Entities.GoodsMan;
using System.Data.Entity.ModelConfiguration;

namespace Hogon.Store.Models.FluentAPI.GoodsMan
{
    public class InstructionConfiguration : EntityTypeConfiguration<Instruction>
    {

        public InstructionConfiguration()
        {
            Property(p => p.Subject).HasMaxLength(20);
            Property(p => p.Author).HasMaxLength(20);
            Property(p => p.AppIndustry).HasMaxLength(20);
            Property(p => p.Usage).HasMaxLength(20);
        }
    }
}
