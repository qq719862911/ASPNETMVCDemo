using System;
using System.Collections.Generic;

namespace MVC2015.DataProvider.MVC2015DB.Models
{
    public partial class tbl_AuditLog
    {
        public int AuditId { get; set; }
        public string ModuleName { get; set; }
        public string OpereationType { get; set; }
        public string OldData { get; set; }
        public string NewData { get; set; }
        public string Operator { get; set; }
        public Nullable<System.DateTime> OperationDate { get; set; }
        public string EntryID { get; set; }
    }
}
