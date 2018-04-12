using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MVC2015.Web.Model.Common;
using BL = MVC2015.Web.BusinessLogic.Common;

namespace MVC2015.Web.Site.Common
{
    public class PermissionHelper
    {
        public static bool CheckPermission(string area, string controller, string action)
        {
            var user = UserHelper.GetCurrentUser();
            if (user == null)
            {
                return false;
            }

            return BL.Permission.CheckPermission(
                new RouteValue { Area = area, Controller = controller, Action = action },
                user
                );
        }

        public static bool CheckUserOfGasStation(int userId, int gasStationId)
        {
            bool result = false;
            using (var perBL = new BL.Permission())
            {
                result = perBL.CheckUserOfGasStation(userId, gasStationId);
            }
            return result;
        }

        public static bool CheckUserOfCompany(int userId, int companyId)
        {
            bool result = false;
            using (var perBL = new BL.Permission())
            {
                result = perBL.CheckUserOfCompany(userId, companyId);
            }
            return result;
        }
    }
}