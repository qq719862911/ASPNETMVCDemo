using System;
using System.Collections.Generic;

namespace MVC2015.DataProvider.MVC2015DB.Models
{
    public partial class tbl_Company
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Nullable<System.TimeSpan> ProcessingTime { get; set; }
        public Nullable<System.TimeSpan> SendTime { get; set; }
        public string SendUrl { get; set; }
        public string EmailAddress { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> Fee { get; set; }
        public string DocumentMaker { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}
