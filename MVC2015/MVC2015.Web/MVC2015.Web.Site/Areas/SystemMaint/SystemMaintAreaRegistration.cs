using System.Web.Mvc;

namespace MVC2015.Web.Areas.SystemMaint
{
    public class SystemMaintAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SystemMaint";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SystemMaint_default",
                "SystemMaint/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}