using System;
using System.Collections.Generic;

namespace MVC2015.DataProvider.MVC2015DB.Models
{
    public partial class tbl_Job_Master
    {
        public tbl_Job_Master()
        {
            this.tbl_Job_History = new List<tbl_Job_History>();
        }

        public long JobId { get; set; }
        public int MCUId { get; set; }
        public int JobTypeId { get; set; }
        public string Operator { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public System.DateTime StatusDate { get; set; }
        public bool IsEXSPutACK { get; set; }
        public Nullable<int> RetryCount { get; set; }
        public Nullable<int> Automatic { get; set; }
        public virtual ICollection<tbl_Job_History> tbl_Job_History { get; set; }
    }
}
