using System;
using System.Collections.Generic;

namespace MVC2015.DataProvider.MVC2015DB.Models
{
    public partial class tbl_SystemCode
    {
        public int ID { get; set; }
        public string CodeType { get; set; }
        public string Code { get; set; }
        public string ResourceKey { get; set; }
        public Nullable<int> Parent { get; set; }
        public Nullable<int> Value { get; set; }
    }
}
