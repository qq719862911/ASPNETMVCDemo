#region using dll
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Globalization;
using System.Threading;
using System.IdentityModel.Services;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.Xml.Linq;
using System.Configuration;
using MVC2015.Web.Model.Account.Login;

using MVC2015.Web.Site.Common;
using MVC2015.Web.Model.Account;
using BL = MVC2015.Web.BusinessLogic.Account;
using MVC2015.Web.Model.SystemMaint.UserInfo;
using MVC2015.Utility.Resource;
using MVC2015.Web.Filter;
using System.Threading.Tasks;
using MVC2015.Web.Site.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using MVC2015.Web.BusinessLogic.SystemMaint;
using MVC2015.Web.BusinessLogic.Account;
using System.Security.Claims;
using Newtonsoft.Json;
using MVC2015.Utility;

#endregion

namespace MVC2015.Web.Site.Areas.Account.Controllers
{
    [NoCache]
    [AllowAnonymous]
    public class LoginController : Controller
    {
        public readonly UserManager<IdentityModel> UserManager = new UserManager<IdentityModel>(new UserStore());
        //
        // GET: /Account/Login/
        //[WarmupAttribute]
        public ActionResult Index(LoginModel model)
        {
            Login login = new Login();
            var failTimes = login.GetLastHourLoginFailTimes(Request.UserHostAddress);
            ViewBag.LoginFailTimes = failTimes;
            var user = UserHelper.GetCurrentUser();
            if (user != null)
            {
                //var gasStationMap = GasStationMap.GetGasStationMap();
                //if (user.CurGasStationId == 0 && gasStationMap.Count > 0)
                //{
                //    return Redirect("~/home/selectgs");
                //}
                //else
                //{
                    return Redirect("~/");
                //}
            }
            SetValidationCode(model, failTimes);
            if (model.UserName == null)
            {
                ModelState.Clear();
                return View(model);

            }
            else
            {
                return View(model);
            }
        }

        /// <summary>
        /// Set default validation code
        /// </summary>
        /// <param name="model"></param>
        private void SetValidationCode(LoginModel model, int failTimes)
        {
            int iFailTimes = failTimes;
            if (iFailTimes < 2)
            {
                model.ValidationCode = "#";
            }
            else
            {
                model.ValidationCode = string.Empty;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(LoginModel model, string url)
        {
            BL.Login login = new BL.Login();
            string ipAddress = Request.UserHostAddress;
            var user = await UserManager.FindAsync(model.UserName, model.EncryptPassword);
            var failTimes = login.GetLastHourLoginFailTimes(Request.UserHostAddress);
            if (user != null)
            {
                user.CompanyId = login.GetCompanyId(user.Id);
                if (failTimes >= 3 && CompareValidationCode(model.ValidationCode))
                {
                    ModelState.AddModelError("", ResourceHelper.GetValue("Message_Common_Login_WorngValidationCode"));
                }
                else
                {
                    await UserHelper.SignInAsync(user, false);
                    login.AddValidSuccessHistory(model.UserName, model.EncryptPassword, ipAddress);
                    return Redirect("~/" + url);
                }
            }
            else
            {

                if (failTimes >= 3 && CompareValidationCode(model.ValidationCode))
                {
                    ModelState.AddModelError("", ResourceHelper.GetValue("Message_Common_Login_WorngValidationCode"));
                }
                ModelState.AddModelError("", ResourceHelper.GetValue("Message_Common_Login_WorngNameOrPassword"));

            }
            SetValidationCode(model, failTimes);
            login.AddValidFaileHistory(model.UserName, model.EncryptPassword, ipAddress);
            ViewBag.LoginFailTimes = failTimes + 1;
            SetValidationCodeToCookie();
            return View("Index", model);
        }

        public ActionResult UserRest()
        {
            return View("UserRest");
        }

        [HttpPost]
        public async Task<ActionResult> UserRests(string Email, string UserName)
        {
            BL.Login empBL = new BL.Login();
            if (empBL.ValiableEmail(Email, UserName) == "true")
            {
                return Json("validateEmail", JsonRequestBehavior.AllowGet);
            }
            else if (empBL.ValiableEmail(Email, UserName) == "error")
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
            UserInfo userBL = new UserInfo();

            IPasswordPolicy Password = new RandomPassword();
            string orginalPassword = Password.GeneratePassword();
            string md5Password = HashEncrypt.MD5(orginalPassword);
            UserInfoItem model = new UserInfoItem();
            model = userBL.GetItemByName(UserName);
            IdentityModel user = await UserManager.FindByNameAsync(UserName);
            string password = UserManager.PasswordHasher.HashPassword(md5Password);
            UserStore store = new UserStore();
            var set = store.SetPasswordHashAsync(user, password);
            var results = store.UpdateAsync(user);
            if (results != null)
            {
                userBL.SendEmail(model, orginalPassword, "User_ResetPassword");
                return Json("true", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ValidationCode()
        {

            string code = SetValidationCodeToCookie();

            byte[] bytes = CommonHelper.CreateCheckCodeImage(code);
            return File(bytes, @"image/jpeg");

        }

        private string SetValidationCodeToCookie()
        {
            string code = CommonHelper.GenerateCheckCode();
            HttpCookie codeCookie = new HttpCookie(CommonHelper.ValidationCode_KEY);
            codeCookie.HttpOnly = true;
            codeCookie.Values.Add(CommonHelper.ValidationCode_KEY, CommonHelper.Encrypt(code));
            Response.Cookies.Add(codeCookie);
            return code;
        }
        private bool CompareValidationCode(string input)
        {
            var cookie = Request.Cookies[CommonHelper.ValidationCode_KEY];
            if (cookie == null)
            {
                return true;
            }
            else
            {
                var decrypt = CommonHelper.Decrypt(cookie[CommonHelper.ValidationCode_KEY].ToString());
                return string.Compare(input, decrypt, true) != 0;
            }
        }
        public ActionResult SignOut()
        {
            IAuthenticationManager AuthenticationManager = HttpContext.GetOwinContext().Authentication;
            AuthenticationManager.SignOut();
            return Redirect("~/");
        }
    }
}
