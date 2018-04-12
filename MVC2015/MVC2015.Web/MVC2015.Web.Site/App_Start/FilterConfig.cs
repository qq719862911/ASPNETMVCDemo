using MVC2015.Web.Site.Filter;
using System.Web;
using System.Web.Mvc;

namespace MVC2015.Web.Site
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
            filters.Add(new PermissionAttribute());
        }
    }
}
