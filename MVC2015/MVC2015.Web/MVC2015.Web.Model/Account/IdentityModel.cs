using Microsoft.AspNet.Identity;
using MVC2015.Utility.Resource;
using MVC2015.Web.Model.Common;
using MVC2015.Web.Model.Permission;
using MVC2015.Web.Model.SystemMaint.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVC2015.Web.Model.Account
{
    public class IdentityModel : IUser
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string LogonName { get; set; }

        public string PasswordHash { get; set; }

        public string StrStatus { get; set; }
        public Nullable<int> RoleId { get; set; }
        public List<CompanyIdModel> CompanyId { get; set; }
        public string UserCompangValue { get; set; }
        public string UserCompanyName { get; set; }
        public string UserGasStationValue { get; set; }
        public string UserGasStationName { get; set; }
        public string DomainAccount { get; set; }
        public string EmailAddress { get; set; }
        public string UpdateBy { get; set; }
        public string CreateBy { get; set; }
        public int CurGasStationId { get; set; }
        public string CurGasStationName { get; set; }
        public string RoleName { get; set; }
        public string ReturnUrl { get; set; }

        public IdentityModel()
        {
            var Switchlist = new List<KeyValueModel>
	        {
				    new KeyValueModel { Text = ResourceHelper.GetValue("SM_UserInfo_UserStatus_Normal"), Value = "1"},
				    new KeyValueModel { Text = ResourceHelper.GetValue("SM_UserInfo_UserStatus_Forbidden"), Value = "0" }
	        };
            StatusList = Switchlist;
            RoleSelectList = new List<RolePermission>();
            GasStationSelectList = new List<SelectListItem>();
            CompanySelectList = new List<SelectListItem>();
        }

        public List<RolePermission> RoleSelectList { get; set; }
        public List<SelectListItem> GasStationSelectList { get; set; }
        public List<SelectListItem> CompanySelectList { get; set; }
        public IEnumerable<KeyValueModel> StatusList { get; set; }
        public List<SiteMenu> siteMenus { get; set; }
    }
}
