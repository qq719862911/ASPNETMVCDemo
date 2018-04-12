//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

////using VM = MVC2015.Web.Model.SystemMaint.SystemCode;
////using BL = MVC2015.Web.BusinessLogic.SystemMaint;
//using MVC2015.Utility.Resource;

//namespace MVC2015.Web.Site.Common
//{
//    public static class SystemCodeHelper
//    {
//        private static List<VM.SystemCodeItem> listAll;
//        private static void GetAllList()
//        {
//            using (var empBL = new BL.SystemCode())
//            {
//                if (listAll == null)
//                {
//                    listAll = empBL.GetItems().ToList();
//                }
//            }
//        }

//        public static List<SelectListItem> GetSelectList(List<SelectListItem> list, string codeType)
//        {
//            GetAllList();
//            List<VM.SystemCodeItem> listType = listAll.Where(s => s.CodeType == codeType).ToList();
//            foreach (var item in listType)
//            {
//                string text = ResourceHelper.GetValue(item.ResourceKey);
//                list.Add(new SelectListItem { Text = text, Value = (item.ID.ToString()) });
//            }
//            return list;
//        }

//        public static List<SelectListItem> GetSelectList(List<SelectListItem> list, string codeType, int parentValue)
//        {
//            GetAllList();
//            List<VM.SystemCodeItem> listType = listAll.Where(s => s.CodeType == codeType && s.Parent == parentValue).ToList();
//            foreach (var item in listType)
//            {
//                string text = ResourceHelper.GetValue(item.ResourceKey);
//                list.Add(new SelectListItem { Text = text, Value = (item.ID.ToString()) });
//            }
//            return list;
//        }

//        public static List<SelectListItem> GetSelectList(string codeType)
//        {
//            GetAllList();
//            List<SelectListItem> listSelect = new List<SelectListItem>();
//            List<VM.SystemCodeItem> listType = listAll.Where(s => s.CodeType == codeType).ToList();
//            foreach (var item in listType)
//            {
//                string text = ResourceHelper.GetValue(item.ResourceKey);
//                listSelect.Add(new SelectListItem { Text = text, Value = (item.ID.ToString()) });
//            }
//            return listSelect;
//        }

//        public static string GetResourceById(int id)
//        {
//            string dispalayValue = string.Empty;
//            GetAllList();
//            VM.SystemCodeItem item = new VM.SystemCodeItem();
//            item = listAll.FirstOrDefault(m => m.ID == id);
//            if (item != null)
//                dispalayValue = ResourceHelper.GetValue(item.ResourceKey);
//            return dispalayValue;
//        }

//        public static List<VM.SystemCodeItem> GetItems(string codeType)
//        {
//            using (var empBL = new BL.SystemCode())
//            {
//                return empBL.GetItems(codeType).ToList();

//            }
//        }

//        public static List<SelectListItem> GetSelectItems(string codeType)
//        {
//            using (var empBL = new BL.SystemCode())
//            {
//                return empBL.GetSelectItems(codeType);

//            }
//        }

//    }
//}