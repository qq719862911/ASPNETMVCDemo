using System.Web;
using System.Web.Optimization;

namespace MVC2015.Web.Site
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/bootstrap-datetimepicker/bootstrap-datetimepicker.js",
                        "~/Scripts/bootstrap-datetimepicker/locales/bootstrap-datetimepicker.zh-CN.js",
                        "~/Scripts/messenger/messenger.js",
                        "~/Scripts/jquery.bpopup.js",
                        "~/Scripts/common.js",
                        "~/Scripts/action.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/jsontree").Include(
                        "~/Scripts/jsonTreeViewer.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap/bootstrap-theme.css",
                      "~/Content/Site.css",
                      "~/Content/Main.css",
                      "~/Content/NavBar.css",
                     "~/Content/bootstrap-datetimepicker/bootstrap-datetimepicker.css",
                    "~/Content/messenger/messenger.css",
                    "~/Content/messenger/messenger-theme-block.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/themes/default/css").Include(
                "~/Content/themes/default/Main.css"));

            BundleTable.Bundles.Add(new StyleBundle("~/Content/bootstrap/base").Include(
                "~/Content/bootstrap/bootstrap.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap/IE8respond").Include(
                "~/Scripts/html5shiv.js",
                "~/Scripts/respond.min.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap/IE7base").Include(
                "~/Content/bootstrapIE7.css"));

            bundles.Add(new ScriptBundle("~/Scripts/md5").Include(
                "~/Scripts/md5.js"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
