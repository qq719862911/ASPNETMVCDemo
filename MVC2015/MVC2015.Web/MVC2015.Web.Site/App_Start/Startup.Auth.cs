using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;

using MVC2015.Web.Site.Models;
using MVC2015.Utility.Common.Web;
using MVC2015.Web.Site.Common;

namespace MVC2015.Web.Site
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            var blnForceHttps = false;
            if (ConfigurationHelper.GetAppSetting(BasicParam.ForceHttps)
                .Equals("true", System.StringComparison.OrdinalIgnoreCase))
            {
                blnForceHttps = true;
            }

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = UserHelper.COOKIENAME + DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                CookieHttpOnly = true,
                CookieSecure = blnForceHttps ? CookieSecureOption.Always : CookieSecureOption.Never
            });


            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication();
        }
    }
}