using System;
using System.Collections.Generic;

namespace MVC2015.DataProvider.MVC2015DB.Models
{
    public partial class tbl_Job_HistoryData
    {
        public int JobDataId { get; set; }
        public int JobHistoryId { get; set; }
        public string DataTypeName { get; set; }
        public string DataValue { get; set; }
        public virtual tbl_Job_History tbl_Job_History { get; set; }
    }
}
