using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2015.Web.Model.SystemMaint.FileUpload
{
    public class FileItem
    {
        public int FileId { get; set; }
        public Guid FileGuid { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public long? FileSize { get; set; }
        public byte[] FileContent { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
