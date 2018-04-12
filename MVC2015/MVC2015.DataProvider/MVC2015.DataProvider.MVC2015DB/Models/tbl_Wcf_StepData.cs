using System;
using System.Collections.Generic;

namespace MVC2015.DataProvider.MVC2015DB.Models
{
    public partial class tbl_Wcf_StepData
    {
        public long WcfStepDataId { get; set; }
        public long WcfStepId { get; set; }
        public string DataTypeName { get; set; }
        public string DataValue { get; set; }
        public virtual tbl_Wcf_Step tbl_Wcf_Step { get; set; }
    }
}
