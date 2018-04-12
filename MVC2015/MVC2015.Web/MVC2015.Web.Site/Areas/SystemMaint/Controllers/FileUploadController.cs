using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VM = MVC2015.Web.Model.SystemMaint.FileUpload;
using BL = MVC2015.Web.BusinessLogic.SystemMaint;
using DevExpress.Web.Mvc;
using MVC2015.Web.Site.Common;
using MVC2015.Web.Filter;

namespace MVC2015.Web.Areas.SystemMaint.Controllers
{
    [NoCache]
    public class FileUploadController : Controller
    {
        // GET: SystemMaint/FileUpload
        public ActionResult Index()
        {
            ViewData["Guid"] = Guid.NewGuid();
            BL.FileUpload fuBL= new BL.FileUpload();
            var model = new VM.FileModel();
            model.List = fuBL.GetItem();

            return View(model);
        }

        public ActionResult GridView()
        {
            BL.FileUpload fuBL = new BL.FileUpload();
            var list = fuBL.GetItem();
            return PartialView("_FileList", list);
        }

        public ActionResult UploadFile(Guid guid)
        {
            BL.FileUpload fuBL= new BL.FileUpload();
            var uploadFiles = UploadControlExtension.GetUploadedFiles("FileUploadControl", UploadHelper.CommonUploadValidationSettings, UploadHelper.FileUploadComplete).FirstOrDefault();

            string fileDesc = GetFileDescFromCookie(guid);


            if (uploadFiles.FileName != "")
            {
                var model = new VM.FileItem()
                {
                    FileGuid = Guid.NewGuid(),
                    FileName = uploadFiles.FileName,
                    FileSize = uploadFiles.ContentLength,
                    FileContent = uploadFiles.FileBytes,
                    Description = fileDesc,
                    CreatedBy = "sysAdmin",//UserHelper.GetCurrentUser().LogonName,
                    CreatedDate = DateTime.Now
                };

                fuBL.Save(model);
            }
            return null;
        }

        [HttpPost]
        public ActionResult PostAttachmentDescription(Guid guid, string fileDesc)
        {
            Dictionary<Guid, string> dictionary = CommonHelper.ReadObjectInCookie<Dictionary<Guid, string>>("FileUploadDesc");
            if (dictionary != null)
            {
                if (dictionary.ContainsKey(guid))
                {
                    dictionary[guid] = fileDesc.Trim();
                }
                else
                {
                    dictionary.Add(guid, fileDesc.Trim());
                }
            }
            else
            {
                dictionary = new Dictionary<Guid, string>() { { guid, fileDesc.Trim() } };
            }
            CommonHelper.SaveObjectInCookie("FileUploadDesc", dictionary);

            return null;
        }

        public ActionResult DeleteFile(int id)
        {
            BL.FileUpload fuBL = new BL.FileUpload();
            bool result = fuBL.Delete(id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FileDownload(Guid guid)
        {
            BL.FileUpload fuBL = new BL.FileUpload();
            var model = fuBL.GetItemByGuid(guid);
            if (model == null)
            {
                return Redirect(Url.Action("Index", "Error", new { area = "" }));
            }

            return File(model.FileContent, "application/octet-stream", Url.Encode(model.FileName));
        }

        private string GetFileDescFromCookie(Guid guid)
        {
            string fileDesc = string.Empty;
            Dictionary<Guid, string> dictionary = CommonHelper.ReadObjectInCookie<Dictionary<Guid, string>>("FileUploadDesc");
            if (dictionary != null)
            {
                fileDesc = dictionary[guid];
            }

            return fileDesc;
        }
    }
}