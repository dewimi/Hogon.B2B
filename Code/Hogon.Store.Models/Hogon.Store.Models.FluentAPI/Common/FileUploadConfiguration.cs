using System.Data.Entity.ModelConfiguration;
using Hogon.Store.Models.Entities.Common;

namespace Hogon.Store.Models.FluentAPI.Common
{
    public class FileUploadConfiguration: EntityTypeConfiguration<FileUpload>
    {

        public FileUploadConfiguration()
        {
            Property(e => e.FileName).HasMaxLength(50);
            Property(e => e.FileType).HasMaxLength(50);
            Property(e => e.FileSize).HasMaxLength(50);

          
        }
    }
}
