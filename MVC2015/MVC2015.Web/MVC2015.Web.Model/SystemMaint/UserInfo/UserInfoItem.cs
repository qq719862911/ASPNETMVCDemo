using MVC2015.Web.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC2015.Web.Model.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MVC2015.Utility.Resource;


namespace MVC2015.Web.Model.SystemMaint.UserInfo
{
    public class UserInfoItem : BaseModel
    {
        public UserInfoItem()
        {
            SelectListItem itemSelect = new SelectListItem();
            itemSelect.Text = ResourceHelper.GetValue("Common_Please_Select");
            itemSelect.Value = "-1";
            itemSelect.Selected = true;
            
            var Switchlist = new List<KeyValueModel>
	        {
				    new KeyValueModel { Text = ResourceHelper.GetValue("SM_UserInfo_UserStatus_Normal"), Value = "1"},
				    new KeyValueModel { Text = ResourceHelper.GetValue("SM_UserInfo_UserStatus_Forbidden"), Value = "0" }
	        };
            StatusList = Switchlist;
            RoleSelectList = new List<SelectListItem>();
            GasStationSelectList = new List<SelectListItem>();
            CompanySelectList = new List<SelectListItem>();

            CompanySelectList.Insert(0, itemSelect);
            RoleSelectList.Insert(0, itemSelect);
        }

        public int UserId { get; set; }
        [Required(ErrorMessage = "SMM_UserInfo_LogonName")]
        [StringLength(100, ErrorMessage = "SMM_UserInfo_LogonName_Length")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "SMM_UserInfo_LogonName_Edit")]
        public string LogonName { get; set; }
        [StringLength(100, ErrorMessage = "SMM_UserInfo_DomainAccount_Length")]
        public string DomainAccount { get; set; }
        [Required(ErrorMessage = "SMM_UserInfo_UserName")]
        [StringLength(100, ErrorMessage = "SMM_UserInfo_UserName_Length")]
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<int> Status { get; set; }
        [KeyValue(DisplayProperty = "StatusList")]
        public string StrStatus { get; set; }
        public IEnumerable<KeyValueModel> StatusList { get; set; }
        [Required(ErrorMessage = "SMM_UserInfo_EmailAddress")]
        //[EmailAddress(ErrorMessage = "邮箱地址的格式不正确")]
        [EmailEx(ErrorMessage = "SMM_UserInfo_FormatEmailAddress")]
        [StringLength(100, ErrorMessage = "SMM_UserInfo_EmailAddress_Length")]
        public string EmailAddress { get; set; }

        public string RoleName { get; set; }
        public Nullable<int> RoleId { get; set; }
        public Nullable<bool> RoleIsDeleted { get; set; } 
        //public Nullable<int> CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string UserGasStationName { get; set; }
        public string UserGasStationValue { get; set; }
        //public virtual ICollection<tbl_Common_UserOfGasStation> tbl_Common_UserOfGasStation { get; set; }
        public string UserCompanyName { get; set; }
        public string UserCompanyValue { get; set; }
        public List<SelectListItem> RoleSelectList { get; set; }
        public List<SelectListItem> GasStationSelectList { get; set; }
        public List<SelectListItem> CompanySelectList { get; set; }
    }
}
