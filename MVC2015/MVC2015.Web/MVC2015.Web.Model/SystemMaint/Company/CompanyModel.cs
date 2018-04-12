using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC2015.Web.Model.Common;
using System.Web.Mvc;
using MVC2015.Web.Model.DataAnnotations;

namespace MVC2015.Web.Model.SystemMaint.Company
{
    public class CompanyModel : BaseModel
    {
        public CompanyModel()
        {
            ProcessingHourSelectList = CompanySelectListItem.GetHourList();
            ProcessingMinuteSelectLsit = CompanySelectListItem.GetMinuteList();
            SendHourSelectList = CompanySelectListItem.GetHourList();
            SendMinuteSelectLsit = CompanySelectListItem.GetMinuteList();
        }
        public int ID { get; set; }
        [Required(ErrorMessage = "SMM_Company_Name")]
        [StringLength(50, ErrorMessage = "SMM_Company_NameLength")]
        [RegularExpression(@"[a-zA-Z0-9\u4e00-\u9fa5_\-\.]+$", ErrorMessage = "SMM_Company_Name_Regular")]
        public string Name { get; set; }

        [Required(ErrorMessage = "SMM_Company_Code")]
        [StringLength(50, ErrorMessage = "SMM_Company_Code_Length")]
        public string Code { get; set; }

        [StringLength(300, ErrorMessage = "SMM_Company_Descripiton")]
        public string Description { get; set; }

        public TimeSpan? ProcessingTime { get; set; }
        public TimeSpan? SendTime { get; set; }

       
        public string SendUrl { get; set; }

        [StringLength(200, ErrorMessage = "SMM_Company_EmailAddress_Length")]
        public string EmailAddress { get; set; }

        public Nullable<bool> IsDeleted { get; set; }
        public List<MVC2015.Web.Model.SystemMaint.UserInfo.UserInfoItem> List { get; set; }
        public Nullable<int> ProcessingHour { get; set; }

        public Nullable<int> ProcessingMinute { get; set; }

        public Nullable<int> SendHour { get; set; }

     
        public Nullable<int> SendMinute { get; set; }

   
        public Nullable<decimal> Rate { get; set; }

     
        public Nullable<decimal> Fee { get; set; }

        public string DocumentMaker { get; set; }
        public List<SelectListItem> ProcessingHourSelectList { get; set; }
        public List<SelectListItem> ProcessingMinuteSelectLsit { get; set; }
        public List<SelectListItem> SendHourSelectList { get; set; }
        public List<SelectListItem> SendMinuteSelectLsit { get; set; }
    }
}
