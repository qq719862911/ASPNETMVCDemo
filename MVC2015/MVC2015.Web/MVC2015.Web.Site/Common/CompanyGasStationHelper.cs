//using MVC2015.Web.Model.Common;
//using MVC2015.Utility.Common;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using BL = MVC2015.Web.BusinessLogic.SystemMaint;
//using VM = MVC2015.Web.Model.SystemMaint;

//namespace MVC2015.Web.Site.Common
//{
//    public class CompanyGasStationHelper
//    {
//        public static void GetCompanyList(List<SelectListItem> listSelect)
//        {
//            using (var empBL = new BL.Company())
//            {
//                List<VM.Company.CompanyModel> listCompany = new List<VM.Company.CompanyModel>();
//                VM.Company.CompanySearch search = new VM.Company.CompanySearch();
//                search.UserId = CommonMethod.getIntValue(UserHelper.GetCurrentUser().Id);
//                search.RoleId = UserHelper.GetCurrentUser().RoleId.Value;
//                listCompany = empBL.GetItems(search).ToList();

//                foreach (var item in listCompany)
//                {
//                    listSelect.Add(new SelectListItem { Text = item.Name.ToString(), Value = item.ID.ToString() });
//                }
//            }
//        }

//        public static VM.Company.CompanyModel GetCurCompanyNameByGasStationId(int id)
//        {
//            using (var empBL = new BL.Company())
//            {
//                VM.Company.CompanyModel model = new VM.Company.CompanyModel();

//                model = empBL.GetCurCompanyById(id);
//                return model;
//            }


//        }

//        public static void GetGasStationList(List<SelectListItem> listSelect)
//        {
//            using (var empBL = new BL.GasStationInfo())
//            {
//                List<VM.GasStationInfo.GasStationInfoItem> listGasStation = new List<VM.GasStationInfo.GasStationInfoItem>();
//                VM.GasStationInfo.GasStationInfoSearch search = new VM.GasStationInfo.GasStationInfoSearch();
//                search.UserId = CommonMethod.getIntValue(UserHelper.GetCurrentUser().Id);
//                search.RoleId = UserHelper.GetCurrentUser().RoleId.Value;
//                if (search.RoleId==(int)EnumRole.SiteAdmin)
//                    listGasStation = empBL.GetItems(search).ToList();
//                else
//                    listGasStation = empBL.GetItemsByUserId(search).ToList();

//                foreach (var item in listGasStation)
//                {
//                    listSelect.Add(new SelectListItem { Text = item.Name.ToString(), Value = item.GasStationId.ToString() });
//                }
//            }
//        }
//    }
//}