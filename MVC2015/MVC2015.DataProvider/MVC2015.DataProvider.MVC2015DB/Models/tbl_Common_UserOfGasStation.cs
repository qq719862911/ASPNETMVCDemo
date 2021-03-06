using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC2015.DataProvider.MVC2015DB.Models
{
    public partial class tbl_Common_UserOfGasStation
    {
        [Key]
        public int Id { get; set; }
        public int GasStationId { get; set; }
        public int UserId { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public virtual tbl_Common_User tbl_Common_User { get; set; }
    }
}
