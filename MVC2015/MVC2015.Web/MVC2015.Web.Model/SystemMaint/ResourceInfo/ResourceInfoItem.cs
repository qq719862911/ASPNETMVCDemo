using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2015.Web.Model.SystemMaint.ResourceInfo
{
    public class ResourceInfoItem
    {
        [Required(ErrorMessage = "SMM_ResourceInfo_ResourceKey")]
        [StringLength(500, ErrorMessage = "SMM_ResourceInfo_ResourceKeyLength")]
        [RegularExpression(@"^\w+$", ErrorMessage = "SMM_ResourceInfo_ResourceKeyRegular")]
        public string ResourceKey { get; set; }

        public string ResourceKey_Old { get; set; }

        [StringLength(1000, ErrorMessage = "SMM_ResourceInfo_ResourceValueZHCNLength")]
        public string ResourceValueZHCN { get; set; }

        [StringLength(1000, ErrorMessage = "SMM_ResourceInfo_ResourceValueZHHKLength")]
        public string ResourceValueZHHK { get; set; }

        [StringLength(1000, ErrorMessage = "SMM_ResourceInfo_ResourceValueENUSLength")]
        public string ResourceValueENUS { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

    }
}

