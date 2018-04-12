using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTX = MVC2015.DataProvider.MVC2015DB.Contexts;
using VM = MVC2015.Web.Model.SystemMaint.FileUpload;
using MD = MVC2015.DataProvider.MVC2015DB.Models;

namespace MVC2015.Web.BusinessLogic.SystemMaint
{
    public class FileUpload
    {
        private readonly CTX.MVC2015DBContext Ctx;

        public FileUpload()
        {
            Ctx = new CTX.MVC2015DBContext();
        }

        public FileUpload(CTX.MVC2015DBContext context)
        {
            Ctx = context;
        }

        public IQueryable<VM.FileItem> GetItem()
        {
            var item = from a in Ctx.tbl_Attachment.Where(t=>t.IsDeleted!=true)
                   select new VM.FileItem(){
                       FileId = a.AttachmentId,
                       FileGuid = a.AttachmentGuid,
                       FileName = a.FileName,
                       FileSize = a.FileSize,
                       Description = a.Description
                   };

            return item;
        }

        public void Save(VM.FileItem item)
        {
            var entity = new MD.tbl_Attachment()
            {
                AttachmentGuid = item.FileGuid,
                FileName = item.FileName,
                FileContent = item.FileContent,
                FileSize = item.FileSize,
                Description = item.Description,
                IsDeleted = false,
                CreatedBy = item.CreatedBy,
                CreatedDate = item.CreatedDate
            };
            Ctx.tbl_Attachment.Add(entity);
            Ctx.SaveChanges();
        }

        public bool Delete(int id)
        {
            var entity = Ctx.tbl_Attachment.FirstOrDefault(t => t.AttachmentId == id);
            if (entity != null)
            {
                Ctx.tbl_Attachment.Remove(entity);
                int count = Ctx.SaveChanges();
                if (count > 0)
                    return true;
            }

            return false;
        }

        public VM.FileItem GetItemByGuid(Guid guid)
        {
            
            var entity = Ctx.tbl_Attachment.FirstOrDefault(t => t.AttachmentGuid == guid);
            if (entity != null)
            {
                var fileItem = new VM.FileItem();
                fileItem.FileName = entity.FileName;
                fileItem.FileContent = entity.FileContent;
                return fileItem;
            }
            else
            {
                return null;
            }
        }
    }
}
