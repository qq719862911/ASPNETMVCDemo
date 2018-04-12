using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2015.Web.Model.SystemMaint.RoleInfo
{
    public class RoleInfoItem
    {
        public int RoleId { get; set; }
        [Required(ErrorMessage = "SMM_RoleInfo_Name")]
        [StringLength(100, ErrorMessage = "SMM_RoleInfo_Name_Length")]
        [RegularExpression(@"[a-zA-Z\u4e00-\u9fa5_\-\.]+$", ErrorMessage = "SMM_RoleInfo_Name_Regular")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public List<RolePermission> RolePermissions { get; set; }

    }
}
