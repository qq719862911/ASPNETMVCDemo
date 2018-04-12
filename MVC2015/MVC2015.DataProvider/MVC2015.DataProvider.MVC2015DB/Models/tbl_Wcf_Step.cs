using System;
using System.Collections.Generic;

namespace MVC2015.DataProvider.MVC2015DB.Models
{
    public partial class tbl_Wcf_Step
    {
        public tbl_Wcf_Step()
        {
            this.tbl_Wcf_StepData = new List<tbl_Wcf_StepData>();
            this.tbl_Wcf_StepException = new List<tbl_Wcf_StepException>();
        }

        public long WcfStepId { get; set; }
        public long WcfId { get; set; }
        public string StepCode { get; set; }
        public long StepDuration { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public short Status { get; set; }
        public virtual tbl_Wcf tbl_Wcf { get; set; }
        public virtual ICollection<tbl_Wcf_StepData> tbl_Wcf_StepData { get; set; }
        public virtual ICollection<tbl_Wcf_StepException> tbl_Wcf_StepException { get; set; }
    }
}
