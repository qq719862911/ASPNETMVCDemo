using System;
using System.Collections.Generic;

namespace MVC2015.DataProvider.MVC2015DB.Models
{
    public partial class tbl_Attachment
    {
        public int AttachmentId { get; set; }
        public Guid AttachmentGuid { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public Nullable<long> FileSize { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }
        
    }
}
