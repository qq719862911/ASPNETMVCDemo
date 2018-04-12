using MVC2015.Web.Filter;
using MVC2015.Web.Model.SystemMaint.ResourceInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL = MVC2015.Web.BusinessLogic.SystemMaint;
using MVC2015.Web.Site.Common;
using MVC2015.Utility.Resource;

namespace MVC2015.Web.Areas.SystemMaint.Controllers
{
    [NoCache]
    public class ResourceInfoController : Controller
    {
        //
        // GET: /SystemMaint/Resource/
        public ActionResult Index()
        {
            var model = new ResourceInfoModel();
            BL.ResourceInfo BL = new BL.ResourceInfo();
            model.List = BL.GetList();
            //if (Request.IsAjaxRequest())
            //{
            //    return PartialView("_List", model.List);
            //}
            return View(model);
        }

        //public ActionResult GridView()
        //{
        //    string json = Request.Params["searchModel"];
        //    BL.ResourceInfo empBL = new BL.ResourceInfo();
        //    var model = empBL.GetList();
        //    SetViewBage();
        //    return PartialView("_List", model);
        //}

        public ActionResult GetList()
        {
            ViewBag.DisplayDate = BasicParam.DatetimeFormat;
            BL.ResourceInfo riBL = new BL.ResourceInfo();
            var model = riBL.GetList();
            return PartialView("_List", model);
        }

        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(ResourceInfoItem model)
        {
            BL.ResourceInfo riBL = new BL.ResourceInfo();
            model.CreatedBy = UserHelper.GetCurrentUser().LogonName;
            if (ModelState.IsValid)
            {
                if (!riBL.Create(model))
                {
                    //ModelState.AddModelError("ResourceKey", ResourceHelper.GetValue("SM_Resource_Key"));
                    return Content("Exist");
                }
                else
                {
                    return Content("Success");
                }
            }
            return View();
        }

        public ActionResult Edit(string id)
        {
            BL.ResourceInfo riBL = new BL.ResourceInfo();
            var model = riBL.GetResourceItemById(id);
            model.ResourceKey_Old = model.ResourceKey;
            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(ResourceInfoItem model)
        {
            BL.ResourceInfo riBL = new BL.ResourceInfo();
            model.UpdatedBy = UserHelper.GetCurrentUser().LogonName;
            var edit = riBL.Update(model);
            if (edit)
            {
                return Content("Success");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            BL.ResourceInfo riBL = new BL.ResourceInfo();
            string UpdatedBy = UserHelper.GetCurrentUser().LogonName;
            bool result = riBL.Delete(id, UpdatedBy);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public void SetViewBage()
        {
            ViewBag.DisplayDate = BasicParam.DatetimeFormat;
        }
	}
}