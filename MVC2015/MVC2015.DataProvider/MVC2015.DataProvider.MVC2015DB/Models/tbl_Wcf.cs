using System;
using System.Collections.Generic;

namespace MVC2015.DataProvider.MVC2015DB.Models
{
    public partial class tbl_Wcf
    {
        public tbl_Wcf()
        {
            this.tbl_Wcf_Step = new List<tbl_Wcf_Step>();
        }

        public long WcfId { get; set; }
        public long JobId { get; set; }
        public int MCUId { get; set; }
        public string WCFMethodName { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public virtual ICollection<tbl_Wcf_Step> tbl_Wcf_Step { get; set; }
    }
}
