//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc;
//using System.Xml.Serialization;
//using MVC2015.Web.Model.Account;
//using MVC2015.Web.Model.Common;
//using MVC2015.Web.Model.Permission;
//using MVC2015.Web.Model.SystemMaint.Company;
//using MVC2015.Web.BusinessLogic.Account;
//using MVC2015.Web.Site.Common;
//using Microsoft.AspNet.Identity;
//using Microsoft.Owin.Security;

//namespace MVC2015.Web.Site.Filter
//{
//    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
//    public class WarmupAttribute : ActionFilterAttribute
//    {
//        public readonly UserManager<IdentityModel> UserManager = new UserManager<IdentityModel>(new UserStore());
//        public override async void OnActionExecuting(ActionExecutingContext filterContext)
//        {
//            System.Diagnostics.Debug.WriteLine("in sign function");
//            var users = HttpContext.Current.User.Identity as ClaimsIdentity;
//            //if (!users.IsAuthenticated)
//            //{
//            if (HttpContext.Current.Request.Headers["GUID"] == MyPageRequester.ID.ToString())
//            {
//                Login BL = new Login();
//                IAuthenticationManager AuthenticationManager = HttpContext.Current.GetOwinContext().Authentication;
//                var user = BL.GetLoginByName("sysadmin");
//                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
//                var identity = await UserManager.CreateIdentityAsync(
//                   user, UserHelper.COOKIENAME + DefaultAuthenticationTypes.ApplicationCookie);

//                #region Serializer user.rolelist
//                XmlSerializer xml = new XmlSerializer(typeof(List<RolePermission>));
//                StringBuilder sb = new StringBuilder();
//                StringWriter sw = new StringWriter(sb);
//                xml.Serialize(sw, user.RoleSelectList);
//                string rolelist = sb.ToString();
//                #endregion

//                #region Serializer user.CompanyId
//                XmlSerializer xmlcompanyId = new XmlSerializer(typeof(List<CompanyIdModel>));
//                StringBuilder sbcompanyId = new StringBuilder();
//                StringWriter swcompanyId = new StringWriter(sbcompanyId);
//                xmlcompanyId.Serialize(swcompanyId, user.CompanyId);
//                string companyId = sbcompanyId.ToString();
//                #endregion

//                identity.AddClaim(new System.Security.Claims.Claim("Role", rolelist));
//                identity.AddClaim(new System.Security.Claims.Claim("RoleName", user.RoleName));
//                identity.AddClaim(new System.Security.Claims.Claim("RoleId", user.RoleId.ToString()));
//                identity.AddClaim(new System.Security.Claims.Claim("CompanyId", companyId));
//                identity.AddClaim(new System.Security.Claims.Claim("CurGasStationId", user.CurGasStationId.ToString()));
//                identity.AddClaim(new System.Security.Claims.Claim("CurGasStationName", user.CurGasStationName == null ? "" : user.CurGasStationName.ToString()));
//                identity.AddClaim(new System.Security.Claims.Claim("LogonName", user.LogonName));
//                AuthenticationManager.SignIn(
//                   new AuthenticationProperties()
//                   {
//                       IsPersistent = false
//                   }, identity);
//                System.Diagnostics.Debug.WriteLine("sign in");
//            }
//            //}
//            base.OnActionExecuting(filterContext);
//        }
//    }
//}