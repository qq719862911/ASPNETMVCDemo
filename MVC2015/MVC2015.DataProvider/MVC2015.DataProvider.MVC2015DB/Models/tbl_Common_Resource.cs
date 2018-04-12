using System;
using System.Collections.Generic;

namespace MVC2015.DataProvider.MVC2015DB.Models
{
    public partial class tbl_Common_Resource
    {
        public string ResourceKey { get; set; }
        public string ResourceValueZHCN { get; set; }
        public string ResourceValueZHHK { get; set; }
        public string ResourceValueENUS { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
