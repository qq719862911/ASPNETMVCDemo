using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MVC2015.Web.Site.Common;
using BL = MVC2015.Web.BusinessLogic.Common;
using VM = MVC2015.Web.Model.Common;
using MVC2015.Web.Model.Common;
using MVC2015.Web.Model.Permission;
using System.Configuration;
using MVC2015.Utility.Common.Web;

namespace MVC2015.Web.Site.Filter
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class PermissionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //IE 过滤
            this.GetBrowserVersions(filterContext);

            var user = UserHelper.GetCurrentUser();
            var siteAdminName = VM.EnumRole.SiteAdmin.ToString().ToLower();
            if (user != null && !user.RoleName.ToLower().Equals(siteAdminName))
            {
                var blnAllow = false;
                var routeValue = new VM.RouteValue();
                var values = filterContext.RouteData.Values;

                routeValue.Area = filterContext.RouteData.DataTokens.ContainsKey("area") ? filterContext.RouteData.DataTokens["area"].ToString().ToLower() : string.Empty;
                routeValue.Controller = values.ContainsKey("controller") ? values["controller"].ToString().ToLower() : string.Empty;
                routeValue.Action = values.ContainsKey("action") ? values["action"].ToString().ToLower() : string.Empty;

                blnAllow = BL.Permission.CheckPermission(routeValue, user);
                if (!blnAllow)
                {
                    if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.Result = new ContentResult { Content = @"抱歉,你不具有当前操作的权限！" };
                    }
                    else
                    {
                        UrlHelper url = new UrlHelper(filterContext.RequestContext);
                        var path = url.Action("Index", "NoPermissions", new { area = "" });
                        filterContext.RequestContext.HttpContext.Response.Redirect(path);
                    }
                }

            }
            else if (user == null)
            {
                UrlHelper url = new UrlHelper(filterContext.RequestContext);
                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest()
                    && filterContext.RequestContext.HttpContext.Request.Url.AbsolutePath != url.Action("UserRests", "Login"))
                {
                    filterContext.Result = new JavaScriptResult { Script = "<script>location.href='/'</script>" };
                }
            }

            base.OnActionExecuting(filterContext);
        }

        private void GetBrowserVersions(ActionExecutingContext filterContext)
        {
            HttpBrowserCapabilities hbc = HttpContext.Current.Request.Browser;
            string browserName = hbc.Browser.ToString().ToLower();     //获取浏览器类型
            string browserVersion = hbc.Version.ToString();    //获取版本号
            BrowserVersionType re = BrowserVersionType.Permit;
            if (!string.IsNullOrEmpty(ConfigurationHelper.GetAppSetting("DefaulBrowserIsAllow")))
            {
                if (!Convert.ToBoolean(ConfigurationHelper.GetAppSetting("DefaulBrowserIsAllow")))
                {
                    re = BrowserVersionType.Forbid;
                }
            }
            object obj = ConfigurationManager.GetSection("BrowserVersionSection");

            BrowserVersionSection mySection4 = (BrowserVersionSection)ConfigurationManager.GetSection("BrowserVersionSection");

            List<MyKeyValueSetting> ml = new List<MyKeyValueSetting>();
            List<BrowserVersion> blist = new List<BrowserVersion>();
            if (mySection4 != null && mySection4.KeyValues.Count > 0)
            {
                ml = (from kv in mySection4.KeyValues.Cast<MyKeyValueSetting>()
                      // let s = string.Format("{0}={1}", kv.Key, kv.Value)
                      select kv).Where(c => c.BrowserName.ToLower() == browserName).ToList();
                foreach (var item in ml)
                {
                    BrowserVersion bv = new BrowserVersion();
                    bv.BrowserName = item.BrowserName;
                    bv.Version = item.Version;
                    bv.IsAllow =Convert.ToBoolean(item.IsAllow);
                    bv.SpecifiedVersion =Convert.ToBoolean(item.specified);
                    blist.Add(bv);
                }
            }
            BrowserVersionType type = BL.Permission.ComparativeVersion(browserVersion, re, blist);

            if (type == BrowserVersionType.Forbid)
            {
                filterContext.Result = new ContentResult { Content = MVC2015.Utility.Resource.ResourceHelper.GetValue("BrowserCanNotUse") };
            }

            CommonHelper.SetCookie("BrowserVersionType", ((int)type).ToString(), null, false);
        }
    }
}