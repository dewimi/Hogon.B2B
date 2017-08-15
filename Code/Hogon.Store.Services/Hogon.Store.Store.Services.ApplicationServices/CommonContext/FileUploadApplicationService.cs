using AutoMapper;
using Hogon.Framework.Core.Owin;
using Hogon.Framework.Core.UnitOfWork;
using Hogon.Store.Models.Dto.Common;
using Hogon.Store.Models.Entities.Common;
using Hogon.Store.Repositories.Common;
using System;

namespace Hogon.Store.Services.ApplicationServices.Common
{
    public class FileUploadApplicationService : BaseApplicationService
    {

        FileUploadRepository fileuploadReps;

        public FileUploadApplicationService()
        {
            fileuploadReps = new FileUploadRepository();
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="dtoFile"></param>
        /// <returns></returns>
        public Guid SevaFile(DtoFileUpload dtoFile)
        {

            FileUpload file = new FileUpload();
            Mapper.Initialize(cfg => cfg.CreateMap<DtoFileUpload, FileUpload>());
            var fileData = Mapper.Map<FileUpload>(dtoFile);
            fileData.FileName = dtoFile.FileName;
            fileData.FileSize = dtoFile.FileSize;
            fileData.FileType = dtoFile.FileType;
            fileData.Url = dtoFile.Url;

            fileuploadReps.Add(fileData);
            Commit();
            return fileData.Id;

        }
    }
}
