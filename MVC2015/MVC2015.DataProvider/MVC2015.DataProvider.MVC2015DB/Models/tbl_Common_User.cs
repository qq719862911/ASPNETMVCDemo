using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC2015.DataProvider.MVC2015DB.Models
{
    public partial class tbl_Common_User
    {
        public tbl_Common_User()
        {
            this.tbl_Common_CustomerField = new List<tbl_Common_CustomerField>();
            this.tbl_Common_RoleUser = new List<tbl_Common_RoleUser>();
            this.tbl_Common_UserOfGasStation = new List<tbl_Common_UserOfGasStation>();
        }

        [Key]
        public int UserId { get; set; }
        public string LogonName { get; set; }
        public string DomainAccount { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<int> Status { get; set; }
        public string EmailAddress { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public virtual ICollection<tbl_Common_CustomerField> tbl_Common_CustomerField { get; set; }
        public virtual ICollection<tbl_Common_RoleUser> tbl_Common_RoleUser { get; set; }
        public virtual ICollection<tbl_Common_UserOfGasStation> tbl_Common_UserOfGasStation { get; set; }
    }
}
