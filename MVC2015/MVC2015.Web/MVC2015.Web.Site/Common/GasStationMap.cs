//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Caching;
//using MVC2015.Web.BusinessLogic;
//using VM = MVC2015.Web.Model.SystemMaint.GasStationInfo;

//namespace MVC2015.Web.Site.Common
//{
//    public static class GasStationMap
//    {
//        private static string CACHE_KEY = "GasStationMap{0}";        

//        public static List<VM.GasStationMap> GetGasStationMap()
//        {
//            var user = UserHelper.GetCurrentUser();
//            if (user == null)
//            {
//                return new List<VM.GasStationMap>();
//            }
//            //var key = string.Format(CACHE_KEY, user.Id);
//            List<VM.GasStationMap> gasStationMap = new List<VM.GasStationMap>();
//            //if (gasStationMap == null)
//            //{
//                using (var bl = new MVC2015.Web.BusinessLogic.Common.GasStationMap())
//                {
//                    gasStationMap = bl.GetGasStationMap(user);
//                }
//                //todo cache
//                //HttpContext.Current.Cache.Insert(key, gasStationMap);
//            //}
//            return gasStationMap;
        
//        }

//        public static List<VM.GasStationMap> GetGasStationMap(string str)
//        {
//            var user = UserHelper.GetCurrentUser();
//            if (user == null)
//            {
//                return new List<VM.GasStationMap>();
//            }
//            //var key = string.Format(CACHE_KEY, user.Id);
//            List<VM.GasStationMap> gasStationMap = new List<VM.GasStationMap>();
//            //if (gasStationMap == null)
//            //{
//                using (var bl = new MVC2015.Web.BusinessLogic.Common.GasStationMap())
//                {
//                    gasStationMap = bl.GetGasStationMap(user,str);
//                }
//                //HttpContext.Current.Cache.Insert(key, gasStationMap);
//            //}
//            return gasStationMap;
//        }

//        public static List<VM.GasStationMap> GetALLGasStationMap()
//        {
//            //var key = "GasAllStationMap";
//            List<VM.GasStationMap> gasStationMap = new List<VM.GasStationMap>();
//            //if (gasStationMap == null)
//            //{
//                using (var bl = new MVC2015.Web.BusinessLogic.Common.GasStationMap())
//                {
//                    gasStationMap = bl.GetAllGasStationMap();
//                }
//                //todo cache
//                //HttpContext.Current.Cache.Insert(CACHE_KEY, gasStationMap);
//            //}
//            return gasStationMap;
//        }

//        public static List<VM.GasStationMap> GetGasStationMapByCompany(string userCompanyValue)
//        {
//            List<VM.GasStationMap> gasStationMap = new List<VM.GasStationMap>();
//            if (userCompanyValue != null)
//            {
//                using (var bl = new MVC2015.Web.BusinessLogic.Common.GasStationMap())
//                {
//                    gasStationMap = bl.GetGasStationMapByCompany(userCompanyValue);
//                }
//            }
//            return gasStationMap;
//        }
//    }
//}