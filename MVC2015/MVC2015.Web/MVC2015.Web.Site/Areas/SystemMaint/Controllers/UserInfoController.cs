using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MVC2015.Web.Model;
using VM = MVC2015.Web.Model.SystemMaint.UserInfo;
using BL = MVC2015.Web.BusinessLogic.SystemMaint;
//using VMRole = MVC2015.Web.Model.SystemMaint.RoleInfo;
using VMCompany = MVC2015.Web.Model.SystemMaint.Company;
using Newtonsoft.Json;
using MVC2015.Web.Site.Common;
using MVC2015.Utility.Resource;
using MVC2015.Web.Filter;
using System.Threading.Tasks;
using MVC2015.Web.Model.Account;
using MVC2015.Web.BusinessLogic.Account;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using MVC2015.Utility;
using MVC2015.Utility.Common;
using System.Text.RegularExpressions;
using MVC2015.Web.Model.DataAnnotations;

namespace MVC2015.Web.Areas.SystemMaint.Controllers
{
    [NoCache]
    public class UserInfoController : Controller
    {
        public readonly UserManager<IdentityModel> UserManager = new UserManager<IdentityModel>(new UserStore());
        //
        // GET: /SystemMaint/UserInfo/
        public ActionResult Index(VM.UserInfoSearch search)
        {
            SetViewBage();
            ViewBag.IsSortable = true;
            var model = new VM.UserInfoModel();
            ViewBag.DisplayDate = BasicParam.DatetimeFormat;
            BL.UserInfo empBL = new BL.UserInfo();
            model.List = empBL.GetItems(search);
            model.Search = search;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", model.List);
            }
            return View(model);
        }

        public ActionResult GridView()
        {
            string json = Request.Params["searchModel"];
            VM.UserInfoSearch searchModel = new VM.UserInfoSearch();
            if (json != null)
            {
                try
                {
                    searchModel = JsonConvert.DeserializeObject(json, typeof(VM.UserInfoSearch)) as VM.UserInfoSearch;
                }
                catch
                {
                    return Json("error", JsonRequestBehavior.AllowGet);
                }
            }
            BL.UserInfo empBL = new BL.UserInfo();
            var model = empBL.GetItems(searchModel);
            ViewBag.DisplayDate = BasicParam.DatetimeFormat;
            return PartialView("_List", model);
        }

        public ActionResult GetDetail(int id)
        {
            BL.UserInfo empBL = new BL.UserInfo();
            if (empBL.GetItemByIdForDelete(id) != null)
            {
                return Redirect(Url.Action("HttpError", "Error", new { area = "" }));
            }
            var model = empBL.GetItemById(id);

            return PartialView("_Detail", model);
        }
        //
        // GET: /SystemMaint/UserInfo/Details/5

        //
        // GET: /SystemMaint/UserInfo/Create
        public ActionResult Create()
        {
            ViewBag.ShowSubmit = true;
            VM.UserInfoItem model = new VM.UserInfoItem();
            model.StrStatus = "1";
            model.RoleSelectList = GetRoleSelectList(model.RoleSelectList);
            //model.CompanySelectList = GetCompanySelectList(model.CompanySelectList);
            BL.UserInfo empBL = new BL.UserInfo();
            //  model.GasStationSelectList = empBL.GetAllGasStationSelectList();
            return View(model);
        }

        //
        // POST: /SystemMaint/UserInfo/Create
        [HttpPost]
        //[AllowAnonymous]
        public async Task<ActionResult> Create(VM.UserInfoItem model)
        {
            BL.UserInfo empBL = new BL.UserInfo();
            if (empBL.ValidateName(-1, model.LogonName))
            {
                return Content(ResourceHelper.GetValue("SMM_UserInfo_SameLogonName"));
            }
            if (!new EmailExAttribute().IsValid(model.EmailAddress))
            {
                return Content("False");
            }
            var user = new IdentityModel();
            user.UserName = model.LogonName;
            user.LogonName = model.UserName;
            user.StrStatus = model.StrStatus;
            user.RoleId = model.RoleId;
            user.UserCompangValue = model.UserCompanyValue;
            user.UserGasStationValue = model.UserGasStationValue;
            user.DomainAccount = model.DomainAccount;
            user.EmailAddress = model.EmailAddress;
            user.CreateBy = "sysAdmin";//UserHelper.GetCurrentUser().LogonName;
            IPasswordPolicy Password = new RandomPassword();
            string orginalPassword = Password.GeneratePassword();
            string md5Password = HashEncrypt.MD5(orginalPassword);
            var result = await UserManager.CreateAsync(user, md5Password);
            if (result.Succeeded)
            {
                if (user.StrStatus == "1")
                {
                    //empBL.SendEmail(model, orginalPassword, "User_New");
                }
                return Content("Success");
            }

            return Content("False");
        }

        //
        // GET: /SystemMaint/UserInfo/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.ShowSubmit = true;
            BL.UserInfo empBL = new BL.UserInfo();
            if (empBL.GetItemByIdForDelete(id) != null)
            {
                return Redirect(Url.Action("HttpError", "Error", new { area = "" }));
            }
            var model = empBL.GetItemById(id);
            model.RoleSelectList = GetRoleSelectList(model.RoleSelectList);
            //model.CompanySelectList = GetCompanySelectList(model.CompanySelectList);
            return View(model);
        }

        //
        // POST: /SystemMaint/UserInfo/Edit/5
        [HttpPost]
        public ActionResult Edit(VM.UserInfoItem model)
        {
            BL.UserInfo empBL = new BL.UserInfo();
            if (empBL.ValidateName(model.UserId, model.LogonName))
            {
                return Content(ResourceHelper.GetValue("SMM_UserInfo_SameLogonName"));
            }
            if (!new EmailExAttribute().IsValid(model.EmailAddress))
            {
                return Content("False");
            }
            model.UpdatedBy = "sysAdmin";// UserHelper.GetCurrentUser().LogonName;
            empBL.Update(model);
            return Content("Success");

        }


        public ActionResult UserEdit()
        {
            ViewBag.ShowSubmit = true;
            BL.UserInfo empBL = new BL.UserInfo();
            int id = Convert.ToInt32(UserHelper.GetCurrentUser().Id);
            var model = empBL.GetItemById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult UserEdit(VM.UserInfoItem model)
        {
            if (!new EmailExAttribute().IsValid(model.EmailAddress))
            {
                return Content("False");
            }
            BL.UserInfo empBL = new BL.UserInfo();
            var userEdit = empBL.GetItemById(model.UserId);
            userEdit.UserName = model.UserName;
            userEdit.EmailAddress = model.EmailAddress;
            userEdit.UpdatedBy = "sysAdmin";//UserHelper.GetCurrentUser().LogonName;
            empBL.UserUpdate(userEdit);

            return Content("Success");
        }

        public ActionResult UpdatePassword()
        {
            ViewBag.ShowSubmit = true;
            BL.UserInfo empBL = new BL.UserInfo();
            int id = Convert.ToInt32(UserHelper.GetCurrentUser().Id);
            var model = empBL.GetItemById(id);
            VM.UserInfoUpdatePassword updtaePassword = new VM.UserInfoUpdatePassword();
            updtaePassword.UserId = model.UserId;
            return View(updtaePassword);
        }

        [HttpPost]
        //[AllowAnonymous]
        public async Task<ActionResult> UpdatePassword(VM.UserInfoUpdatePassword modelNew)
        {
            if (modelNew.UserId != Convert.ToInt32(UserHelper.GetCurrentUser().Id))
            {
                return Redirect(Url.Action("Index", "NoPermissions", new { area = "" }));
            }
            BL.UserInfo empBL = new BL.UserInfo();
            VM.UserInfoItem model = new VM.UserInfoItem();
            // model.UserId = CommonMethod.getIntValue(UserHelper.GetCurrentUser().Id);
            model = empBL.GetItemById(model.UserId);
            IdentityModel user = await UserManager.FindByNameAsync(model.LogonName);
            //modelNew.Password = HashEncrypt.MD5(modelNew.Password);
            var results = await UserManager.CheckPasswordAsync(user, modelNew.Password);
            if (results)
            {
                //modelNew.NewPassword = HashEncrypt.MD5(modelNew.NewPassword);
                string password = UserManager.PasswordHasher.HashPassword(modelNew.NewPassword);
                UserStore store = new UserStore(UserHelper.GetCurrentUser().LogonName);
                var set = store.SetPasswordHashAsync(user, password);
                var result = store.UpdateAsync(user);
                if (user != null)
                {
                    return Content("Success");
                }
            }
            else
            {
                return Content("Error");
            }
            return Content("False");
        }

        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword(int id)
        {
            BL.UserInfo empBL = new BL.UserInfo();
            if (empBL.GetItemByIdForDelete(id) != null)
            {
                return Redirect(Url.Action("HttpError", "Error", new { area = "" }));
            }
            IPasswordPolicy Password = new RandomPassword();
            string orginalPassword = Password.GeneratePassword();
            orginalPassword = "123456";
            string md5Password = HashEncrypt.MD5(orginalPassword);
            VM.UserInfoItem model = new VM.UserInfoItem();
            model = empBL.GetItemById(id);
            //IdentityModel user = await UserManager.FindByNameAsync(model.LogonName);
            //string password = UserManager.PasswordHasher.HashPassword(md5Password);
            //UserStore store = new UserStore(UserHelper.GetCurrentUser().LogonName);
            //var set = store.SetPasswordHashAsync(user, password);
            //var result = store.UpdateAsync(user);

            model.Password = md5Password;
            empBL.UpdatePasswordDb(model);
            if (model != null)
            {
                // empBL.SendEmail(model, orginalPassword, "User_ResetPassword");
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /SystemMaint/UserInfo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            BL.UserInfo empBL = new BL.UserInfo();
            if (empBL.GetItemByIdForDelete(id) != null)
            {
                return Redirect(Url.Action("HttpError", "Error", new { area = "" }));
            }
            //if (id.ToString() == UserHelper.GetCurrentUser().Id)
            //{
            //    return Json("own", JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
                bool result = empBL.Delete(id, "sysAdmin");//UserHelper.GetCurrentUser().LogonName);
                return Json(result, JsonRequestBehavior.AllowGet);
            //}
        }

        public void SetViewBage()
        {
            ViewBag.DisplayDate = BasicParam.ShortDatetimeFormat;
        }

        private List<SelectListItem> GetRoleSelectList(List<SelectListItem> list)
        {
            BL.UserInfo emp = new BL.UserInfo();
            List<SelectListItem> allRoleSelectList = emp.GetAllItems();
            //  var roleSelectList = (from a in allRoleSelectList select a.Text).Distinct().ToList();
            foreach (var item in allRoleSelectList)
            {
                list.Add(new SelectListItem { Text = item.Text, Value = item.Value });
            }
            return list;
        }

        public ActionResult GetGasStationMap(int id, string value)
        {
            BL.UserInfo empBL = new BL.UserInfo();
            VM.UserInfoItem model = new VM.UserInfoItem();
            if (id > 0)
            {
                model = empBL.GetItemById(id);
            }
            if (value != null)
            {
                //   model = empBL.GetItemByCompany(model, value);
            }
            model.UserCompanyValue = value;
            return PartialView("_GasStationMap", model);
        }

        [HttpPost]
        public ActionResult GetGasStationMaps(int id, string value)
        {
            BL.UserInfo empBL = new BL.UserInfo();
            VM.UserInfoItem model = new VM.UserInfoItem();
            if (id > 0)
            {
                model = empBL.GetItemById(id);
            }
            if (value != null)
            {
                //  model = empBL.GetItemByCompany(model, value);
            }
            model.UserCompanyValue = value;
            return PartialView("_GasStationMap", model);
        }
    }
}
