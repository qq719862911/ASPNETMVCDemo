using System;
using System.Collections.Generic;

namespace MVC2015.DataProvider.MVC2015DB.Models
{
    public partial class tbl_Job_History
    {
        public tbl_Job_History()
        {
            this.tbl_Job_HistoryData = new List<tbl_Job_HistoryData>();
        }

        public int JobHistoryId { get; set; }
        public long JobId { get; set; }
        public int StatusValue { get; set; }
        public System.DateTime StatusStartDate { get; set; }
        public System.DateTime StatusEndDate { get; set; }
        public virtual tbl_Job_Master tbl_Job_Master { get; set; }
        public virtual ICollection<tbl_Job_HistoryData> tbl_Job_HistoryData { get; set; }
    }
}
