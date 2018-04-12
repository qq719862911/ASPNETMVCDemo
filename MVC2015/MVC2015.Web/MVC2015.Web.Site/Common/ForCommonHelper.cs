using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC2015.Web.Site.Common
{
    /// <summary>
    /// for Mock test , can not let CommonHelper inherit IForCommonHelper, because CommonHelper all is static method
    /// </summary>
    public class ForCommonHelper :IForCommonHelper
    {
        public string ToJson(object obj)
        {
            return CommonHelper.ToJson(obj);
        }

        public T ReadObjectInCookie<T>(string key)
        {
            return CommonHelper.ReadObjectInCookie<T>(key);
        }

        public void SaveObjectInCookie(string key, object obj, DateTime? expr = null)
        {
            CommonHelper.SaveObjectInCookie(key, obj, expr);
        }

    }
}