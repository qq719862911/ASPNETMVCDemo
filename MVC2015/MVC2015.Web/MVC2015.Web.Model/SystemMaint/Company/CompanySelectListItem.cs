using MVC2015.Utility.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace MVC2015.Web.Model.SystemMaint.Company
{
    public class CompanySelectListItem
    {
        public static void InsertSelectListItem(ref List<SelectListItem> list)
        {
            SelectListItem itemSelect = new SelectListItem();
            itemSelect.Text =ResourceHelper.GetValue("Common_Please_Select");
            itemSelect.Value = "";
            itemSelect.Selected = true;
            list.Insert(0, itemSelect);
        }

        public static List<SelectListItem> GetHourList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            for (int i = 0; i < 24; i++)
            {
                SelectListItem item = new SelectListItem();
                item.Text = i.ToString();
                item.Value = i.ToString();
                list.Add(item);
            }
            InsertSelectListItem(ref list);
            return list;
        }

        public static List<SelectListItem> GetMinuteList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            for (int i = 0; i < 60; i++)
            {
                SelectListItem item = new SelectListItem();
                item.Text = i.ToString();
                item.Value = i.ToString();
                list.Add(item);
            }
            InsertSelectListItem(ref list);
            return list;
        }
    }
}
