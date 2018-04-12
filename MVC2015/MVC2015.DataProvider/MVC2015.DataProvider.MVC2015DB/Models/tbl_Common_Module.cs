using System;
using System.Collections.Generic;

namespace MVC2015.DataProvider.MVC2015DB.Models
{
    public partial class tbl_Common_Module
    {
        public tbl_Common_Module()
        {
            this.tbl_Common_ModulePermissionDefine = new List<tbl_Common_ModulePermissionDefine>();
            this.tbl_Common_RolePermissionConfig = new List<tbl_Common_RolePermissionConfig>();
        }

        public int ModuleId { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string ResourceKey { get; set; }
        public string Description { get; set; }
        public int MenuGroupOrder { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsMenu { get; set; }
        public virtual ICollection<tbl_Common_ModulePermissionDefine> tbl_Common_ModulePermissionDefine { get; set; }
        public virtual ICollection<tbl_Common_RolePermissionConfig> tbl_Common_RolePermissionConfig { get; set; }
    }
}
