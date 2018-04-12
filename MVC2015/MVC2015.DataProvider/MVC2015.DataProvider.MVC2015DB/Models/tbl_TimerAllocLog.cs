using System;
using System.Collections.Generic;

namespace MVC2015.DataProvider.MVC2015DB.Models
{
    public partial class tbl_TimerAllocLog
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public System.DateTime Datetime { get; set; }
        public string RunningTimerId { get; set; }
        public string MachineName { get; set; }
    }
}
