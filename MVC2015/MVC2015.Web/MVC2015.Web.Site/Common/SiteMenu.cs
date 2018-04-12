//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Caching;
//using MVC2015.Web.BusinessLogic;
//using VM = MVC2015.Web.Model.Permission;

//namespace MVC2015.Web.Site.Common
//{
//    public static class SiteMenu
//    {
//        private static string CACHE_KEY = "SiteMenus{0}";
//        //Cache Sliding Expiration time(Minute)
//        private const int SlidingExpiration = 20;
//        public static List<VM.SiteMenu> GetSiteMenu()
//        {
//            var user = UserHelper.GetCurrentUser();
//            if (user == null)
//            {
//                return new List<VM.SiteMenu>();
//            }
//            var key = string.Format(CACHE_KEY, user.RoleId);
//            List<VM.SiteMenu> siteMenus = HttpContext.Current.Cache[key] as List<VM.SiteMenu>;
//            if (siteMenus == null || siteMenus.Count <= 0)
//            {
//                var blSiteMenu = new MVC2015.Web.BusinessLogic.Common.SiteMenu();
//                siteMenus = blSiteMenu.GetSiteMenu(user);
//                blSiteMenu.Dispose();
//                //todo cache
//                //HttpContext.Current.Cache.Insert(key, siteMenus, CacheDependencyHelper.GetCacheDependency(new string[] { BasicParam.RolePermissionCacheKey }), Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(SlidingExpiration));
//                //HttpContext.Current.Cache.Insert(key, siteMenus, CacheDependencyHelper.GetCacheDependency(new string[] { BasicParam.RolePermissionCacheKey }));
//            }
//            var IsGasStationSelected = !string.IsNullOrEmpty(user.CurGasStationName);
//            return MVC2015.Web.BusinessLogic.Common.SiteMenu.CloneSiteMenu(siteMenus, IsGasStationSelected);
//        }
//    }
//}