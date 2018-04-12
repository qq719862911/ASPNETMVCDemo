//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Routing;
////using MVC2015.Utility.Web;
////using MVC2015.Web.Common;
////using VM = MVC2015.Web.Model.Permission;

//namespace MVC2015.Web
//{
//    public static class SiteMenu
//    {
//        public static MvcHtmlString Menu(this HtmlHelper htmlHelper, List<VM.SiteMenu> siteMenus)
//        {
//            string Area = HttpContext.Current.Request.RequestContext.RouteData.Values.ContainsKey("area") ? HttpContext.Current.Request.RequestContext.RouteData.Values["area"].ToString() : string.Empty;
//            string Controller = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
//            string Action = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
//            string companyCode = HttpContext.Current.Request.RequestContext.RouteData.Values.ContainsKey("company") ? HttpContext.Current.Request.RequestContext.RouteData.Values["company"].ToString() : string.Empty;
//            int? companyId = 0;

//            companyId = companyId.HasValue ? companyId.Value : -1;

//            StringBuilder MenuTag = new StringBuilder(200);

//            if (siteMenus != null)
//            {
//                foreach (VM.SiteMenu p in siteMenus)
//                {
//                    MenuTag.Append(GetTagLI(htmlHelper, p, Area, Controller, Action, companyCode, 0, companyId.Value));
//                }
//            }

//            return new MvcHtmlString(MenuTag.ToString());
//        }

//        static string GetTagLI(HtmlHelper htmlHelper, VM.SiteMenu menu, string area, string controller, string action, string companyCode, int menuLevel, int companyId)
//        {
//            TagBuilder LiTag = new TagBuilder("li");
//            string activeCSS = string.Empty;
//            string dropdownCSS = "dropdown";
//            string caret = "caret";
//            string strUrl = UrlHelper.GenerateUrl(null, menu.ActionName, menu.Controller, null, null, null, new RouteValueDictionary(new { area = menu.Area }), htmlHelper.RouteCollection, htmlHelper.ViewContext.RequestContext, true);

//            StringBuilder LiInnerHtml = new StringBuilder(200);

//            if (menu.Area.Equals(area, StringComparison.InvariantCultureIgnoreCase)
//                && menu.Controller.Equals(controller, StringComparison.InvariantCultureIgnoreCase)
//                && menu.ActionName.Equals(action, StringComparison.InvariantCultureIgnoreCase))
//            {
//                activeCSS = "active";
//            }

//            if (menuLevel >= 1)
//            {
//                dropdownCSS = "dropdown-submenu";
//                caret = "submenuCaret";
//            }

//            menuLevel++;

//            //NamingItems NamingItems = new NamingItems(companyId);
//            //if(System.Web.HttpRuntime.Cache["Menu"] == null)
//            //{
//            //    NamingItems=new NamingItems(companyId);
//            //    System.Web.HttpRuntime.Cache["Menu"] = NamingItems;
//            //}
//            //else
//            //{
//            //    NamingItems=(NamingItems)System.Web.HttpRuntime.Cache["Menu"];
//            //}

//            if (menu.ChildrenMenu != null && menu.ChildrenMenu.Count > 0)
//            {
//                //<li class="@activeCSS @dropdownCSS" ><a href="@Url.Action(Model.ActionName, Model.Controller, new { area = Model.Area })" class="dropdown-toggle" data-toggle="dropdown" >@ResourceHelper.GetResourceWithCustom(typeof(CommonResource),Model.ResourceKey,companyCode)
//                //     <b class="@caret"></b></a>
//                //    @Html.Partial("_NavBarMenuNodes", Model.ChildrenMenu)
//                //</li>
//                TagBuilder aTag = new TagBuilder("a");
//                TagBuilder bTag = new TagBuilder("b");
//                StringBuilder aInnerHtml = new StringBuilder(200);

//                LiTag.AddCssClass(string.Format("{0} {1}", activeCSS, dropdownCSS));

//                aTag.MergeAttribute("href", strUrl);
//                aTag.AddCssClass("dropdown-toggle");
//                aTag.MergeAttribute("data-toggle", "dropdown");

//                bTag.AddCssClass(caret);

//                //aInnerHtml.Append(ResourceHelper.GetResourceObject(typeof(CommonResource), menu.ResourceKey));
//                aInnerHtml.Append(MVC2015.Utility.Resource.ResourceHelper.GetValue(menu.ResourceKey));

//                //aInnerHtml.Append(NamingItems.GetNamingFormatValueFromDefaultDisplayType(menu.ResourceKey));
//                aInnerHtml.Append(bTag.ToString(TagRenderMode.Normal));

//                aTag.InnerHtml = aInnerHtml.ToString();

//                LiInnerHtml.Append(aTag.ToString(TagRenderMode.Normal));

//                LiInnerHtml.Append(GetTagUL(htmlHelper, menu.ChildrenMenu, area, controller, action, companyCode, menuLevel, companyId));
//            }
//            else
//            {
//                //<li class="@activeCSS" ><a href="@Url.Action(Model.ActionName, Model.Controller, new { area = Model.Area })" >@ResourceHelper.GetResourceWithCustom(typeof(CommonResource),Model.ResourceKey,companyCode)</a></li>
//                TagBuilder aTag = new TagBuilder("a");
//                LiTag.AddCssClass(string.Format("{0}", activeCSS));
//                aTag.MergeAttribute("href", strUrl);
//                //aTag.InnerHtml = ResourceHelper.GetResourceObject(typeof(CommonResource), menu.ResourceKey);
//                aTag.InnerHtml = MVC2015.Utility.Resource.ResourceHelper.GetValue(menu.ResourceKey);
//                //aTag.InnerHtml = NamingItems.GetNamingFormatValueFromDefaultDisplayType(menu.ResourceKey);
//                LiInnerHtml.Append(aTag.ToString(TagRenderMode.Normal));
//            }

//            LiTag.InnerHtml = LiInnerHtml.ToString();

//            return LiTag.ToString(TagRenderMode.Normal);
//        }

//        static string GetTagUL(HtmlHelper htmlHelper, List<VM.SiteMenu> menus, string area, string controller, string action, string companyCode, int menuLevel, int companyId)
//        {
//            string UlHtml = string.Empty;
//            if (menus != null)
//            {
//                TagBuilder UlTag = new TagBuilder("ul");
//                StringBuilder innerHtml = new StringBuilder(200);
//                UlTag.AddCssClass("dropdown-menu");

//                foreach (VM.SiteMenu p in menus)
//                {
//                    innerHtml.Append(GetTagLI(htmlHelper, p, area, controller, action, companyCode, menuLevel, companyId));
//                }

//                UlTag.InnerHtml = innerHtml.ToString();

//                UlHtml = UlTag.ToString(TagRenderMode.Normal);
//            }

//            return UlHtml;
//        }
//    }
//}