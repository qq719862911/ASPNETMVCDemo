using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC2015.Web.Model.SystemMaint.Company;
using MVC2015.Web.BusinessLogic.SystemMaint;
using MVC2015.Web.Model.Common;
using MVC2015.Web.Filter;
//using MVC2015.Resource;
using BL = MVC2015.Web.BusinessLogic.SystemMaint;
using MVC2015.Utility.Common;
using MVC2015.Web.Site.Common;
using MVC2015.Utility.Resource;

namespace MVC2015.Web.Areas.SystemMaint.Controllers
{
    [NoCache]
    public class CompanyController : Controller
    {
        Company BL = new Company();
        // GET: SystemMaint/Company
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetList()
        {
            ViewBag.DisplayDate = BasicParam.DatetimeFormat;
            CompanySearch search = new CompanySearch();
           // search.UserId = CommonMethod.getIntValue(UserHelper.GetCurrentUser().Id);
          //  search.RoleId = (int)UserHelper.GetCurrentUser().RoleId;
            var companyModel = BL.GetItems(search);
            return PartialView("_List", companyModel);
        }

        public ActionResult CompanyUserList()
        {
            return PartialView("");
        }

        public ActionResult Create()
        {
            MVC2015.Web.Model.SystemMaint.Company.CompanyModel model = new CompanyModel();
            model.List = null;
            return View("Create", model);
        }
        [HttpPost]
        public ActionResult Create(CompanyModel model)
        {
            //model.CreatedBy = UserHelper.GetCurrentUser().LogonName;
            //if (BL.ValidateCompanyName(-1, model.Name))
            //{
            //    return Content(ResourceHelper.GetValue("SMM_Company_NameExists"));
            //}
            //if (CompareProcessingTimeAndSendTime(model))
            //{
            //    return Content(ResourceHelper.GetValue("SMM_Company_ProcessingTimeAndSendTime"));
            //}
            var update = BL.Create(model);
            if (update)
            {
                return Content("Success");
            }
            return View();
        }

        public ActionResult Edit(int Id)
        {
            ViewBag.currCompanyId = Id;
         //   int userId = CommonMethod.getIntValue(UserHelper.GetCurrentUser().Id);
           // if (!PermissionHelper.CheckUserOfCompany(userId, Id) && UserHelper.GetCurrentUser().RoleId != (int)EnumRole.SiteAdmin)
             //   return Redirect(Url.Action("Index", "NoPermissions", new { area = "" }));
            if (BL.GetCompanyByIdForDelete(Id) != null)
            {
                return Redirect(Url.Action("HttpError", "Error", new { area = "" }));
            }
            var model = BL.GetCompanyById(Id);
            //ProcessingTime 和 SendTime 转化为对应的 ProcessingHour， ProcessingMinute， SendHour，  SendTimeMinute
          //  getHourAndMinute(model);
            model.List = GetUserItems(Id);
            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(CompanyModel model, string allSelectList, int currCompanyId)
        {
            //model.UpdatedBy = UserHelper.GetCurrentUser().LogonName;
            if (BL.ValidateCompanyName(model.ID, model.Name))
            {
                return Content(ResourceHelper.GetValue("SMM_Company_NameExists"));
            }
            //if (CompareProcessingTimeAndSendTime(model))
            //{
            //    return Content(ResourceHelper.GetValue("SMM_Company_ProcessingTimeAndSendTime"));
            //}
            model.UpdatedBy = "sysadmin";
            var edit = BL.Update(model, allSelectList, currCompanyId, model.UpdatedBy);
            if (edit)
            {
                return Content("Success");
            }
            return View();
        }

        [HttpPost] public ActionResult Delete(int Id)
        {
            if (BL.GetCompanyByIdForDelete(Id) != null)
            {
                return Redirect(Url.Action("HttpError", "Error", new { area = "" }));
            }
            //if (!BL.ValidateDeleteForUser(Id, Convert.ToInt32(UserHelper.GetCurrentUser().Id)))
            //{
            //    return Redirect(Url.Action("Index", "NoPermissions", new { area = "" }));
            //}
            if (BL.ValidateDelete(Id) )//|| UserHelper.GetCurrentUser().LogonName.ToLower() != "sysadmin")
            {
                return Content("HasUser");
            }
            var model = BL.Delete(Id, "sysAdmin");//UserHelper.GetCurrentUser().LogonName);
            if (model)
            {
                return Content("Success");
            }
            return Content("False");
        }

        public ActionResult UserList(List<MVC2015.Web.Model.SystemMaint.UserInfo.UserInfoItem> UserItems, int? companyId)
        {
            if (companyId.HasValue)
            {
                ViewBag.CurrCompanyId = companyId;
            }
            else { ViewBag.CurrCompanyId = 0; }
            List<MVC2015.Web.Model.SystemMaint.UserInfo.UserInfoItem> userItems = UserItems;
            return PartialView("_UserList", userItems);
        }

        private List<MVC2015.Web.Model.SystemMaint.UserInfo.UserInfoItem> GetUserItems(int? companyId)
        {
            BL.UserInfo userBL = new BL.UserInfo();
            List<MVC2015.Web.Model.SystemMaint.UserInfo.UserInfoItem> userItems = new List<Model.SystemMaint.UserInfo.UserInfoItem>();
            if (companyId.HasValue)
            {
                userItems = userBL.GetItemByCompanyId(companyId.Value);
            }
            return userItems;
        }

        public ActionResult GetUserSelectItem(MVC2015.Web.Model.SystemMaint.UserInfo.UserInfoSearch search, int currCompanyId, string currUserIds)
        {
            ViewBag.CurrCompanyId = currCompanyId;
            ViewBag.CurrUserIds = currUserIds;
            BL.UserInfo userBL = new BL.UserInfo();
            MVC2015.Web.Model.SystemMaint.UserInfo.UserInfoModel userModel = new Model.SystemMaint.UserInfo.UserInfoModel();
            userModel.List = userBL.GetAllUserItemForCompany(search);
            userModel.Search = search;
            return PartialView("_UserSelectDialog", userModel);
        }

     
    }
}