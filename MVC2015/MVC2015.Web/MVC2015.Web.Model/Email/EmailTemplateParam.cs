using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;

namespace MVC2015.Web.Model.Email
{
    public class EmailTemplateParam
    {
        public const string CompanyName = "[CompanyName]";

        public const string SystemName = "[SystemName]";

        public const string SystemNameBasic = "ECO ROS";

        public const string UserDisplayName = "[UserDisplayName]";

        public const string URL = "[URL]";

        public const string UserName = "[UserName]";

        public const string Password = "[Password]";

        public const string EquipmentName = "[EquipmentName]";

        public const string Location = "[Location]";

        public const string TeamName = "[TeamName]";

        public const string PlanServiceDate = "[PlanServiceDate]";

        public static string GetHostName()
        {
            // undone: aaron 2014-7-10, should not use ConfigurationManager in Model Layer
            var appurl = MVC2015.Utility.Common.Web.ConfigurationHelper.GetAppSetting("AppUrl");//ConfigurationManager.AppSettings["AppUrl"];
            if (appurl == null || appurl == "")
            {
                return "http://" + HttpContext.Current.Request.Url.Authority.ToString();
            }
            else
            {
                return appurl;
            }

        }
    }

    public class EmailTemplateName
    {
        public const string ServiceOrder_New = "ServiceOrder_New";

        public const string ServiceOrder_NotifyMaintChecker = "ServiceOrder_NotifyMaintChecker";

        public const string ServiceOrder_NotifyMaintLeader_DueSoon = "ServiceOrder_NotifyMaintLeader_DueSoon";

        public const string ServiceOrder_NotifyMaintLeader_NeedRepair = "ServiceOrder_NotifyMaintLeader_NeedRepair";

        public const string ServiceOrder_NotifyMaintLeader_RepairNotCreate = "ServiceOrder_NotifyMaintLeader_RepairNotCreate";

        public const string ServiceOrder_NotifyMaintLeader_WasDue = "ServiceOrder_NotifyMaintLeader_WasDue";

        public const string ServiceOrder_NotifyRepairChecker = "ServiceOrder_NotifyRepairChecker";

        public const string ServiceOrder_NotifyRepairLeader_New = "ServiceOrder_NotifyRepairLeader_New";

        public const string ServiceOrder_NotifyRepairLeader_Unsolved = "ServiceOrder_NotifyRepairLeader_Unsolved";

        public const string User_New = "User_New";

        public const string User_ResetPassword = "User_ResetPassword";

        public const string CalOrder_NotifyCalWorkLeader_DueSoon = "CalOrder_NotifyCalWorkLeader_DueSoon";

        public const string CalOrder_New = "CalOrder_New";

        public const string CalOrder_CheckIn = "CalOrder_CheckIn";

        public const string CalOrder_ExtendService = "CalOrder_ExtendService";

        public const string CalOrder_NotifyCalWorkLeader_WasDue = "CalOrder_NotifyCalWorkLeader_WasDue";

        public const string CalOrder_NotifySE_NeedApprove = "CalOrder_NotifySE_NeedApprove";

        public const string CalOrder_NotifySCO_NeedReview = "CalOrder_NotifySCO_NeedReview";

        public const string TaskOrder_NotifyMaintLeader_DueSoon = "TaskOrder_NotifyMaintLeader_DueSoon";

        public const string TaskOrder_NotifyMaintLeader_WasDue = "TaskOrder_NotifyMaintLeader_WasDue";

        public const string TaskOrder_NotifyMaintChecker = "TaskOrder_NotifyMaintChecker";
    }
}
