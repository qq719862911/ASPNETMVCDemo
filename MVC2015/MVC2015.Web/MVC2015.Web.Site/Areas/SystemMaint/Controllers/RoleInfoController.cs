using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MVC2015.Web.Model;
using VM = MVC2015.Web.Model.SystemMaint.RoleInfo;
using VMSC = MVC2015.Web.Model.SystemMaint.SystemCode;
using BL = MVC2015.Web.BusinessLogic.SystemMaint;
using Newtonsoft.Json;
using MVC2015.Web.Site.Common;
using MVC2015.Utility.Resource;
using MVC2015.Web.Filter;
using MVC2015.Web.Model.SystemMaint.Module;
using MVC2015.Web.Model.SystemMaint.UserInfo;

namespace MVC2015.Web.Areas.SystemMaint.Controllers
{
    [NoCache]
    public class RoleInfoController : Controller
    {
        // GET: SystemMaint/Role
        public ActionResult Index(VM.RoleInfoSearch search)
        {
            SetViewBage();
            ViewBag.IsSortable = true;
            var model = new VM.RoleInfoModel();
            ViewBag.DisplayDate = BasicParam.DatetimeFormat;
            BL.RoleInfo empBL = new BL.RoleInfo();
            model.List = empBL.GetItems(search);
            model.Search = search;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", model.List);
            }
            return View(model);
        }

        //[ChildActionOnly]
        public ActionResult GridView()
        {
            string json = Request.Params["searchModel"];
            VM.RoleInfoSearch searchModel = new VM.RoleInfoSearch();
            if (json != null)
            {
                try
                {
                    searchModel = JsonConvert.DeserializeObject(json, typeof(VM.RoleInfoSearch)) as VM.RoleInfoSearch;
                }
                catch
                {
                    return Json("error", JsonRequestBehavior.AllowGet);
                }
            }
            BL.RoleInfo empBL = new BL.RoleInfo();
            ViewBag.DisplayDate = BasicParam.DatetimeFormat;
            var model = empBL.GetItems(searchModel);
            return PartialView("_List", model);
        }

        // GET: SystemMaint/Role/Details/5
        public ActionResult GetDetail(int id)
        {
            BL.RoleInfo empBL = new BL.RoleInfo();
            if (empBL.GetItemByIdForDelete(id) != null)
            {
                return Redirect(Url.Action("HttpError", "Error", new { area = "" }));
            }
            var model = empBL.GetItemById(id);

            return PartialView("_Detail", model);
        }

        // GET: SystemMaint/Role/Create
        public ActionResult Create()
        {
            ViewBag.ShowSubmit = true;
            VM.RoleInfoItem model = new VM.RoleInfoItem();
            return View(model);
        }

        // POST: SystemMaint/Role/Create
        [HttpPost]
        public ActionResult Create(VM.RoleInfoItem model, List<ModulePermission> ModulePermissions)
        {
            BL.RoleInfo empBL = new BL.RoleInfo();
            if (empBL.ValidateName(-1, model.Name))
            {
                return Content(ResourceHelper.GetValue("SMM_RoleInfo_NameExists"));
            }
            if (empBL.ValidateModulePermissions(ModulePermissions))
            {
                return Content(ResourceHelper.GetValue("SMM_RoleInfo_AddPermissions"));
            }
            model.CreatedBy = UserHelper.GetCurrentUser().LogonName;
            empBL.Create(model, ModulePermissions);

            //CacheDependencyHelper.Change(new string[] { BasicParam.RolePermissionCacheKey });

            return Content("Success");
        }

        // GET: SystemMaint/Role/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.RoleId = id;
            BL.RoleInfo empBL = new BL.RoleInfo();
            var model = empBL.GetItemById(id);
            if (model == null)
            {
                return Redirect(Url.Action("HttpError", "Error", new { area = "" }));
            }
            return View(model);
        }

        // POST: SystemMaint/Role/Edit/5
        [HttpPost]
        public ActionResult Edit(VM.RoleInfoItem model, List<ModulePermission> ModulePermissions, string allSelectList, int currRoleId, string deleteUserIds)
        {
            BL.RoleInfo empBL = new BL.RoleInfo();
            BL.UserInfo userBL = new BL.UserInfo();
            if (empBL.ValidateName(model.RoleId, model.Name))
            {
                return Content(ResourceHelper.GetValue("SMM_RoleInfo_NameExists"));
            }
            if (empBL.ValidateModulePermissions(ModulePermissions))
            {
                return Content(ResourceHelper.GetValue("SMM_RoleInfo_AddPermissions"));
            }
            //empBL.e(model);
            model.UpdatedBy = UserHelper.GetCurrentUser().LogonName;
            empBL.Update(model, ModulePermissions);
            userBL.UpdateUserRole(allSelectList, currRoleId, deleteUserIds, model.UpdatedBy);

            //CacheDependencyHelper.Change(new string[] { BasicParam.RolePermissionCacheKey });

            return Content("Success");
        }


        // POST: SystemMaint/Role/Delete/5

        [HttpPost]
        public ActionResult Delete(int id)
        {
            // TODO: Add delete logic here

            BL.RoleInfo empBL = new BL.RoleInfo();
            if (empBL.GetItemByIdForDelete(id) != null)
            {
                return Redirect(Url.Action("HttpError", "Error", new { area = "" }));
            }
            if (empBL.ValidateDelete(id))
            {
                //return Content("失败");
                return Json("exist", JsonRequestBehavior.AllowGet);
            }
            else
            {
                var model = empBL.Delete(id, UserHelper.GetCurrentUser().LogonName);
                //CacheDependencyHelper.Change(new string[] { BasicParam.RolePermissionCacheKey });
                //return Content("success");
                return Json(model, JsonRequestBehavior.AllowGet);
            }

        }
        public void SetViewBage()
        {
            ViewBag.DisplayDate = BasicParam.ShortDatetimeFormat;
        }

        public ActionResult MoulePermission(int? roleId)
        {
            if (roleId.HasValue)
            {
                ViewBag.RoleId = roleId.Value;
            }
            List<ModuleItem> moduleItems = GetMoudlePermission(roleId);
            return PartialView("_Module", moduleItems);
        }

        public ActionResult MoulePermissionRead(int? roleId)
        {
            List<ModuleItem> moduleItems = GetMoudlePermission(roleId);
            return PartialView("_ModuleRead", moduleItems);
        }

        private List<Model.SystemMaint.Module.ModuleItem> GetMoudlePermission(int? roleId)
        {
            BL.RoleInfo empBL = new BL.RoleInfo();
            BL.Module moduleBL = new BL.Module();
            List<ModuleItem> moduleItems = null;

            if (roleId.HasValue)
            {
                moduleItems = moduleBL.GetHierarchyModuleItem(empBL.GetRolePermission(roleId.Value));
                ViewBag.RoleName = empBL.GetItemById(roleId.Value).Name;
            }
            else
            {
                moduleItems = moduleBL.GetHierarchyModuleItem();
            }
            empBL.Dispose();
            moduleBL.Dispose();

            ModuleItem ModulePermissionList = CreateModulePermission(moduleItems);
            //if(!roleId.HasValue && ModulePermissionList!=null && ModulePermissionList.ModulePermissions!=null)
            //{
            //    ModulePermissionList.ModulePermissions.ForEach(p =>
            //    {
            //        if (p.Value == 1)
            //        {
            //            p.IsSelected = true;
            //        }
            //    });
            //}
            ViewData["ModulePermission"] = ModulePermissionList;
            return moduleItems;
        }

        private ModuleItem CreateModulePermission(List<ModuleItem> moduleItems)
        {
            ModuleItem PermissionItem = new Model.SystemMaint.Module.ModuleItem();
            PermissionItem.ModulePermissions = new List<Model.SystemMaint.Module.ModulePermission>();
            HierarchyModulePermission(moduleItems, PermissionItem);

            return PermissionItem;

        }

        private void HierarchyModulePermission(List<ModuleItem> moduleItems, ModuleItem permissionItem)
        {
            if (moduleItems != null)
            {
                moduleItems.ForEach(p =>
                {
                    permissionItem.ModulePermissions.AddRange(p.ModulePermissions);
                    HierarchyModulePermission(p.ChildModuleItems, permissionItem);
                });
            }
        }

        public ActionResult UserList(int? roleId)
        {
            UserInfoModel usermodel = GetUserItems(roleId);
            ViewBag.RoleId = roleId.Value;
            return PartialView("UserList", usermodel.List);
        }

        public ActionResult UserListRead(int? roleId)
        {
            UserInfoModel usermodel = GetUserItems(roleId);
            ViewBag.RoleId = roleId.Value;
            return PartialView("_UserListRead", usermodel.List);
        }

        private Model.SystemMaint.UserInfo.UserInfoModel GetUserItems(int? roleId)
        {
            BL.UserInfo userBL = new BL.UserInfo();
            UserInfoModel usermodel = new UserInfoModel();
            if (roleId.HasValue)
            {
                usermodel.List = userBL.GetItemByRoleId(roleId.Value);

            }

            return usermodel;
        }

        public ActionResult GetAllUserItem(UserInfoSearch search, int currRoleId, string currUserIds)
        {
            ViewBag.RoleId = currRoleId;
            ViewBag.CurrUserIds = currUserIds;
            BL.UserInfo userBL = new BL.UserInfo();
            UserInfoModel usermodel = new Model.SystemMaint.UserInfo.UserInfoModel();
            usermodel.List = userBL.GetAllItemById(search);
            usermodel.Search = search;
            return PartialView("_UserSelectDialog", usermodel);
        }

    }
}