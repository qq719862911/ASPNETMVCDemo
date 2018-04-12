using System;
using System.Collections.Generic;

namespace MVC2015.DataProvider.MVC2015DB.Models
{
    public partial class tbl_UserLogonHistory
    {
        public int ID { get; set; }
        public string IPAddress { get; set; }
        public string LogonName { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> LogonDate { get; set; }
        public Nullable<int> LogonTimes { get; set; }
    }
}
