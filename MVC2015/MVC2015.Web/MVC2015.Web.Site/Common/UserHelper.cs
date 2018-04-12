using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Routing;
using System.Text;
using System.Xml.Linq;
using System.Security.Cryptography;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;

using VM = MVC2015.Web.Model.Common;
using BL = MVC2015.Web.BusinessLogic.Common;
using System.Web.Caching;
using System.Threading.Tasks;
using MVC2015.Web.Model.Account.Login;
using MVC2015.Web.Site.Models;
using MVC2015.Web.Model.Account;
using System.Security.Claims;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Owin.Security.Cookies;
using MVC2015.Web.Model.Permission;
using MVC2015.Web.Model.SystemMaint.Company;
using System.Web.Helpers;

namespace MVC2015.Web.Site.Common
{
    public static class UserHelper
    {
        //public static readonly UserManager<IdentityModel> UserManager = new UserManager<IdentityModel>(new UserStore());
        public const string COOKIENAME = "Mvc2015.";
        /// <summary>
        /// Get Current User Info from Session State.
        /// If not exist on Session State, then it will use User Name to DB get one.
        /// </summary>
        /// <returns></returns>
        public static IdentityModel GetCurrentUser()
        {
            if (HttpContext.Current == null)
                return null;

            string userName = HttpContext.Current.User.Identity.GetUserName();
            var users = HttpContext.Current.User.Identity as ClaimsIdentity;
            if (users.Claims.Count() < 1 && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (userName != null)
                {
                    var user = new IdentityModel();
                    user = GetCurrentUser(userName, VM.EnumAuthenticationType.Form);
                    return user;
                }
            }
            #region GetCookieValue
            IdentityModel useridentity = null;
            if (users.Claims.Count() > 1)
            {
                useridentity = new IdentityModel();
                #region Deserialize Role
                string cla = users.FindFirstValue("Role").ToString();
                StringReader reader = new StringReader(cla);
                XmlSerializer xml = new XmlSerializer(typeof(List<RolePermission>));
                List<RolePermission> roleList = xml.Deserialize(reader) as List<RolePermission>;
                #endregion
                #region Deserialize CompanyId
                string claCompanyId = users.FindFirstValue("CompanyId").ToString();
                StringReader readerCompanyId = new StringReader(claCompanyId);
                XmlSerializer xmlCompanyId = new XmlSerializer(typeof(List<CompanyIdModel>));
                List<CompanyIdModel> companyId = null;
                #endregion
                try
                {
                    companyId = xmlCompanyId.Deserialize(readerCompanyId) as List<CompanyIdModel>;
                }
                catch (Exception)
                {
                    IAuthenticationManager AuthenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                    AuthenticationManager.SignOut();
                }
                useridentity.Id = users.GetUserId().ToString();
                useridentity.UserName = users.GetUserName();
                useridentity.CurGasStationId = Convert.ToInt32(users.FindFirstValue("CurGasStationId"));
                if (users.FindFirstValue("CurGasStationName") != null)
                {
                    useridentity.CurGasStationName = users.FindFirstValue("CurGasStationName").ToString();
                }
                useridentity.RoleSelectList = roleList;
                useridentity.RoleName = users.FindFirstValue("RoleName").ToString();
                useridentity.RoleId = Convert.ToInt32(users.FindFirstValue("RoleId"));
                useridentity.CompanyId = companyId;
                useridentity.ReturnUrl = users.FindFirstValue("ReturnUrl");
                useridentity.LogonName = users.FindFirstValue("LogonName");
            }
            #endregion
            return useridentity;

        }


        /// <summary>
        /// Use User Name to DB get the User Info data, and save it to Session.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IdentityModel GetCurrentUser(string userName, VM.EnumAuthenticationType type)
        {
            IdentityModel user;
            using (var perBL = new BL.Permission())
            {
                user = perBL.GetUser(userName, type);
                //HACK: only for test purpose.
                if (user == null)
                {
                    FormsAuthentication.SignOut();
                    return null;
                }
            }
            return user;
        }

        public static async Task SignInAsync(IdentityModel user, bool isPersistent)
        {
            UserManager<IdentityModel> UserManager = new UserManager<IdentityModel>(new UserStore());
            IAuthenticationManager AuthenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(
               user, COOKIENAME + DefaultAuthenticationTypes.ApplicationCookie);

            #region Serializer user.rolelist
            XmlSerializer xml = new XmlSerializer(typeof(List<RolePermission>));
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            xml.Serialize(sw, user.RoleSelectList);
            string rolelist = sb.ToString();
            #endregion

            #region Serializer user.CompanyId
            XmlSerializer xmlcompanyId = new XmlSerializer(typeof(List<CompanyIdModel>));
            StringBuilder sbcompanyId = new StringBuilder();
            StringWriter swcompanyId = new StringWriter(sbcompanyId);
            xmlcompanyId.Serialize(swcompanyId, user.CompanyId);
            string companyId = sbcompanyId.ToString();
            #endregion
            //string companyId = "123";
            identity.AddClaim(new System.Security.Claims.Claim("Role", rolelist));
            identity.AddClaim(new System.Security.Claims.Claim("RoleName", user.RoleName));
            identity.AddClaim(new System.Security.Claims.Claim("RoleId", user.RoleId.ToString()));
            identity.AddClaim(new System.Security.Claims.Claim("CompanyId", companyId));
            identity.AddClaim(new System.Security.Claims.Claim("CurGasStationId", user.CurGasStationId.ToString()));
            identity.AddClaim(new System.Security.Claims.Claim("CurGasStationName", user.CurGasStationName == null ? "" : user.CurGasStationName.ToString()));
            identity.AddClaim(new System.Security.Claims.Claim("LogonName", user.LogonName));
            AuthenticationManager.SignIn(
               new AuthenticationProperties()
               {
                   IsPersistent = isPersistent
               }, identity);
        }

        public static async Task SignInAsyncHome(ClaimsIdentity identity, bool isPersistent)
        {
            IAuthenticationManager AuthenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            AuthenticationManager.SignOut();

            AuthenticationManager.SignIn(
               new AuthenticationProperties()
               {
                   IsPersistent = isPersistent
               }, identity);
        }

        /// <summary>
        /// get csrf token
        /// </summary>
        /// <returns></returns>
        public static string GSIMS_TokenHeaderValue()
        {
            string cookieToken = null;
            string formToken = null;
            var dateTime = DateTime.Now.Ticks.ToString();
            AntiForgery.GetTokens(null, out cookieToken, out formToken);
            return string.Format("{0}:{1}:{2}", cookieToken, formToken, CommonHelper.Encrypt(dateTime));
        }
    }
}