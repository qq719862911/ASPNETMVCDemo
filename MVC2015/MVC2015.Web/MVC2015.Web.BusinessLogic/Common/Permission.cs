using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
//using System.Web.Routing;

using VM = MVC2015.Web.Model.Common;
using MVC2015.Web.Model.Permission;
using CTX = MVC2015.DataProvider.MVC2015DB.Contexts;
using MD = MVC2015.DataProvider.MVC2015DB.Models;
using MVC2015.Web.Model.Account;
using MVC2015.Web.Model.Common;

namespace MVC2015.Web.BusinessLogic.Common
{
    public class Permission : IDisposable
    {
        private readonly CTX.PermissionContext ctx;

        public Permission()
        {
            ctx = new CTX.PermissionContext();
        }

        public IdentityModel GetUser(string loginName, VM.EnumAuthenticationType type)
        {
            if (type == VM.EnumAuthenticationType.SAML20)
            {
                var user = ctx.User.Include("tbl_Common_RoleUser").
                    SingleOrDefault(u => u.DomainAccount == loginName && u.IsDeleted != true);
                return ConvertUser(user);
            }
            if (type == VM.EnumAuthenticationType.Form)
            {
                var user = ctx.User.Include("tbl_Common_RoleUser").
                    SingleOrDefault(u => u.LogonName == loginName && u.IsDeleted != true);
                return ConvertUser(user);
            }
            return null;
        }

        private IdentityModel ConvertUser(MD.tbl_Common_User user)
        {
            if (user == null)
            {
                return null;
            }

            var result = new IdentityModel();
            var roleUser = user.tbl_Common_RoleUser.FirstOrDefault();
            int roleId = roleUser == null ? 0 : roleUser.RoleId.Value;
            var role = ctx.Role.Where(r => r.RoleId == roleId).FirstOrDefault();

            result.Id = user.UserId.ToString();
            result.LogonName = user.LogonName;
            result.RoleId = roleId;
            result.RoleName = role == null ? "" : role.Name;
            result.RoleSelectList =
                (from m in ctx.ModulePermissionDefine
                 join pi in ctx.RolePermissionConfig.DefaultIfEmpty() on m.ModuleId equals pi.ModuleId into inner
                 from p in inner.DefaultIfEmpty()
                 where (p.RoleId == roleId || p.RoleId == null)
                 select new RolePermission
                 {
                     Action = string.IsNullOrEmpty(m.Action) ? "index" : m.Action,
                     Area = m.Area ?? string.Empty,
                     Controller = m.Controller ?? string.Empty,
                     ModuleId = m.ModuleId,
                     IsAvailable = (m.Value & p.Value) == m.Value ? true : false
                 }).ToList();

            MD.tbl_Common_UserOfGasStation userGas = ctx.UserOfGasStation.Where(m => m.UserId == user.UserId).FirstOrDefault();
            return result;
        }

        public static bool CheckPermission(VM.RouteValue routeData, IdentityModel user)
        {
            //如果是User的个人信息和修改密码，则跳过
            if (routeData.Action.ToLower() == "useredit" || routeData.Action.ToLower() == "updatepassword")
            {
                return true;
            }
            var result =
                (from p in user.RoleSelectList
                 where
                    p.Area.ToLower() == routeData.Area.ToLower() &&
                    p.Controller.ToLower() == routeData.Controller.ToLower() &&
                    p.Action.ToLower() == routeData.Action.ToLower() &&
                    p.IsAvailable == false
                 select p.ModuleId).FirstOrDefault();
            if (result > 0)
            {
                return false;
            }
            else
            {

                //check for view index permission
                routeData.Action = "index";
                var results =
                (from p in user.RoleSelectList
                 where
                    p.Area.ToLower() == routeData.Area.ToLower() &&
                    p.Controller.ToLower() == routeData.Controller.ToLower() &&
                    p.Action.ToLower() == routeData.Action.ToLower() &&
                    p.IsAvailable == false
                 select p.ModuleId).FirstOrDefault();
                if (results > 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckUserOfGasStation(int userId, int gasStationId)
        {
            int result = this.ctx.UserOfGasStation.Where(m =>
                m.IsDeleted != true &&
                m.UserId == userId &&
                m.GasStationId == gasStationId).Count();
            if (result > 0)
                return true;
            else
                return false;
        }

        public bool CheckUserOfCompany(int userId, int companyId)
        {
            int result = this.ctx.UserOfCompany.Where(m => m.IsDeleted != true &&
                m.UserId == userId &&
                m.CompanyId == companyId).Count();
            if (result > 0)
                return true;
            else
                return false;
        }

        public static BrowserVersionType ComparativeVersion(string versions, BrowserVersionType reDefault, List<BrowserVersion> result)
        {
            Version currentVer = new Version(versions);
            Version itemVer = null;
            BrowserVersionType re = BrowserVersionType.Cue;
            //禁止
            foreach (var item in result)
            {
                itemVer = new Version(item.Version);
                if (item.IsAllow == false)
                {
                    if (item.SpecifiedVersion == true)
                    {
                        if (currentVer == itemVer)
                        {
                            re = BrowserVersionType.Forbid;
                            break;
                        }
                    }
                    else
                    {
                        if (currentVer <= itemVer)
                        {
                            re = BrowserVersionType.Forbid;
                            break;
                        }
                    }
                }
            }
            //許可
            if (re == BrowserVersionType.Cue)
            {
                foreach (var item in result)
                {
                    itemVer = new Version(item.Version);
                    if (item.IsAllow == false)
                    {
                        if (item.SpecifiedVersion == true)
                        {
                            if (currentVer == itemVer) { re = BrowserVersionType.Permit; }
                        }
                        else
                        {
                            if (currentVer >= itemVer) { re = BrowserVersionType.Permit; }
                        }
                    }
                }
            }
            if (re==BrowserVersionType.Cue)//未控制浏览器项取默认设置
            {
                re = reDefault;
            }
            return re;
        }


        #region IDisposable Member
        public void Dispose()
        {
            if (ctx != null)
            {
                ctx.Dispose();
            }
        }
        #endregion
    }
}
