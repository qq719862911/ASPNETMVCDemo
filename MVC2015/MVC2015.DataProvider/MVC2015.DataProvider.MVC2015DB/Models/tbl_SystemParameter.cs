using System;
using System.Collections.Generic;

namespace MVC2015.DataProvider.MVC2015DB.Models
{
    public partial class tbl_SystemParameter
    {
        public string ParamName { get; set; }
        public Nullable<int> IntValue { get; set; }
        public string StringValue { get; set; }
        public Nullable<System.TimeSpan> TimeValue { get; set; }
        public string Description { get; set; }
    }
}
