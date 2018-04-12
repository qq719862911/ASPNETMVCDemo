using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2015.Utility.Resource
{
    public static class ResourceHelper
    {
        private static Dictionary<string, string> CommonResource;
        private static object locker = new object();
        private static void GetCommonResourceList()
        {
            lock (locker)
            {
                if (CommonResource == null || CommonResource.Count == 0)
                {
                    List<ResourceModel> CommonResourceList;
                    ResourceBL empBL = new ResourceBL();
                    CommonResource = new Dictionary<string, string>();
                    CommonResourceList = empBL.GetItems().ToList();
                    foreach (var item in CommonResourceList)
                    {
                        CommonResource.Add(item.ResourceKey, item.ResourceValueZHCN);
                    }
                }
            }
        }

        public static void UpdateCommonResourceList()
        {
            if (CommonResource != null)
                CommonResource.Clear();
            GetCommonResourceList();
        }

        public static string GetValue(string resourceKey)
        {
            if (string.IsNullOrEmpty(resourceKey))
                return resourceKey;
            string str = string.Empty;
            GetCommonResourceList();
            str = CommonResource.FirstOrDefault(d => d.Key.ToLower() == resourceKey.ToLower()).Value;
            if (str == null)
                return resourceKey;
            return str;
        }

        public static string GetValue(string resourceKey, CultureInfo cultureInfo)
        {
            string str = string.Empty;
            GetCommonResourceList();
            str = CommonResource.FirstOrDefault(d => d.Key == resourceKey).Value;
            if (str == null)
            {
                return resourceKey;
            }
            return str;
        }

        public static string GetValue(string resourceKey1, string resourceKey2)
        {
            string str = string.Empty;
            GetCommonResourceList();
            str = CommonResource.FirstOrDefault(d => d.Key.ToLower() == resourceKey1.ToLower()).Value
                + CommonResource.FirstOrDefault(d => d.Key.ToLower() == resourceKey2.ToLower()).Value;
            if (str == null)
            {
                return resourceKey1 + "+" + resourceKey2;
            }
            return str;
        }
    }
}
