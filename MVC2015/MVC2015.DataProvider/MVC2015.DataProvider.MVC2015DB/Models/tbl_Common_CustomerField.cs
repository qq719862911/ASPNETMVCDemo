using System;
using System.Collections.Generic;

namespace MVC2015.DataProvider.MVC2015DB.Models
{
    public partial class tbl_Common_CustomerField
    {
        public int CustomerFieldId { get; set; }
        public int UserId { get; set; }
        public string Area { get; set; }
        public string Controllers { get; set; }
        public string GridView { get; set; }
        public string FieldString { get; set; }
        public virtual tbl_Common_User tbl_Common_User { get; set; }
    }
}
