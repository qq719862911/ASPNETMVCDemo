using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using MVC2015.Utility.Resource;
using MVC2015.Web.Site.Common;


    public class DevexpressGridViewSettings
    {
        private string GridViewName;
        private string endCallBack2;

        public string EndCallBack2
        {
            get
            {
                return string.IsNullOrEmpty(endCallBack2) ? "undefined" : endCallBack2;
            }
            set
            {
                endCallBack2 = value;
            }
        }
        //GridView的EndCallBakc将要执行的js函数名，定义在action.js中
        public string RemerberPageRowSizeScriptFunction
        {
            get
            {
                return "SetGridViewPageRowSize";
            }
        }
        public DevexpressGridViewSettings(string gridViewName)
        {
            GridViewName = gridViewName;
        }

        public GridViewSettings Settings(bool remerberPageRowSize = false)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = GridViewName;
            settings.Width = Unit.Percentage(100);
            //settings.ControlStyle.BorderRight.BorderWidth = 0;
            settings.SettingsPager.AlwaysShowPager = true;
            settings.Settings.ShowFilterRow = true;
            settings.Settings.ShowFilterRowMenu = true;
            settings.Settings.ShowFooter = true;
            settings.Settings.ShowFilterBar = GridViewStatusBarMode.Hidden;
            settings.SettingsBehavior.AllowSelectByRowClick = true;
            settings.SettingsBehavior.EnableRowHotTrack = true;
            settings.SettingsBehavior.ColumnResizeMode = ColumnResizeMode.Control;

            settings.SettingsPager.PageSize = 10;
            settings.SettingsPager.Position = PagerPosition.Bottom;
            settings.SettingsPager.FirstPageButton.Visible = true;
            settings.SettingsPager.LastPageButton.Visible = true;
            settings.SettingsPager.PageSizeItemSettings.Visible = true;
            settings.SettingsPager.PageSizeItemSettings.Items = new string[] { "10", "20", "50" };
            settings.SettingsPager.PageSizeItemSettings.Caption = ResourceHelper.GetValue("Common_GridView_DisplayPerPage");
            //GridView控件需要记录pageRowSize信息
            if (remerberPageRowSize)
            {
                settings.ClientSideEvents.EndCallback = "function(s, e){" + RemerberPageRowSizeScriptFunction + "(s, e," + EndCallBack2 + ");}";
            }
            
            settings.SettingsPager.AllButton.Visible = false;

            settings.SetEmptyDataRowTemplateContent(ResourceHelper.GetValue("Common_GridView_NoData"));
            settings.SettingsPager.Summary.EmptyText = ResourceHelper.GetValue("Common_GridView_NoData");

            #region 设置GridView的PageRowSize
            IForCommonHelper commonHelp = new ForCommonHelper();
            Dictionary<string, int> pageRowSizeModel = commonHelp.ReadObjectInCookie<Dictionary<string, int>>(BasicParam.GridViewPageRowSizeCookiesKey);
            if (pageRowSizeModel != null)
            {
                if (pageRowSizeModel.Any(i => string.Compare(i.Key, GridViewName, false) == 0))
                {
                    settings.SettingsPager.PageSize = pageRowSizeModel[GridViewName];
                }
            }
            #endregion
            return settings;
        }
    }
