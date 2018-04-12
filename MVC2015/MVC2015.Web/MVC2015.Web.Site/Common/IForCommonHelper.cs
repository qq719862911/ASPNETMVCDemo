using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC2015.Web.Site.Common
{
    public interface IForCommonHelper
    {
        T ReadObjectInCookie<T>(string key);
        void SaveObjectInCookie(string key, object obj, DateTime? expr = null);
    }
}