using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hogon.Store.UserInterface.WebSite.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: FileUpload
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 文件保存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FileUpload()
        {
            string uploadUrl = @"~/Content/fileupload";
            string fileUrl = String.Empty;
            string originalFileName = string.Empty;
            HttpPostedFileBase file = Request.Files.Get(0);
            if (file.ContentLength > 0)
            {
                //获取文件名称
                var fileName = Path.GetFileName(file.FileName);
                var fielSize = Path.GetFileName(file.ContentLength.ToString());
                string suffixName = "." + fileName.Substring(fileName.LastIndexOf(".") + 1);
                fileName = System.Guid.NewGuid().ToString() + suffixName;
                fileUrl = uploadUrl + "/" + fileName;
                originalFileName = file.FileName;
                //保存文件
                string FileLocation = Path.Combine(
                Server.MapPath(uploadUrl), fileName);
                file.SaveAs(FileLocation);
            }

            return Json(new { fileUrl = fileUrl, originalFileName = originalFileName });
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