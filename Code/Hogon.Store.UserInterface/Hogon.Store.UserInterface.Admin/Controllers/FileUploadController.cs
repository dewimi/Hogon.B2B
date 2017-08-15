using AutoMapper;
using Hogon.Store.Models.Dto.Common;
using Hogon.Store.Models.Entities.Common;
using Hogon.Store.Services.ApplicationServices.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hogon.Store.UserInterface.Admin.Controllers
{
    public class FileUploadController : Controller
    {
        FileUploadApplicationService fileSvc = new FileUploadApplicationService();
        // GET: FileUpload
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FileUpload()
        {
            string uploadUrl = @"~/Content/fileupload";
            string fileUrl = String.Empty;
            string originalFileName = string.Empty;
            string fileName = string.Empty;
            string fileSize = string.Empty;
            string fileType = string.Empty;
            HttpPostedFileBase file = Request.Files.Get(0);
            if (file.ContentLength > 0)
            {
                //获取文件名称
                fileName = Path.GetFileName(file.FileName);
                fileSize = Path.GetFileName(file.ContentLength.ToString())+"k";
                string suffixName = "." + fileName.Substring(fileName.LastIndexOf(".") + 1);
                fileType = fileName.Substring(fileName.LastIndexOf(".") + 1);
                fileName = System.Guid.NewGuid().ToString() + suffixName;
                fileUrl = uploadUrl + "/" + fileName;
                originalFileName = file.FileName;
                //保存文件
                string FileLocation = Path.Combine(
                Server.MapPath(uploadUrl), fileName);
                file.SaveAs(FileLocation);
            }

            return Json( new { fileUrl = fileUrl, fileName = fileName, fileSize = fileSize, fileType = fileType });
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <returns></returns>
        public ActionResult SeveFile(DtoFileUpload dtoFile)
        {
           var fileId = fileSvc.SevaFile(dtoFile);
            return Json(fileId);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteFile()
        {
            HttpPostedFileBase file = Request.Files.Get(0);
            return Json("");
        }
    }
}