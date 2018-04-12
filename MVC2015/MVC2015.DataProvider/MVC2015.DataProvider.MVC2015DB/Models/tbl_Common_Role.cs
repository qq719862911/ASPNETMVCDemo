using System;
using System.Collections.Generic;

namespace MVC2015.DataProvider.MVC2015DB.Models
{
    public partial class tbl_Common_Role
    {
        public tbl_Common_Role()
        {
            this.tbl_Common_RolePermissionConfig = new List<tbl_Common_RolePermissionConfig>();
        }

        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public virtual ICollection<tbl_Common_RolePermissionConfig> tbl_Common_RolePermissionConfig { get; set; }
    }
}
