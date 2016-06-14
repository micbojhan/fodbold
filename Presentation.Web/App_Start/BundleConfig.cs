using System.Web.Optimization;

namespace Presentation.Web.App_Start
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jslibs").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate*",
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"
            ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"
            ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/fussball.css",
                "~/Content/site.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/angularCore").Include(
                "~/Scripts/angular/angular-1.5.5/angular.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/angularApp").Include(
                "~/Scripts/angular/testAngularApp.js",
                "~/Scripts/angular/testAngularService.js",
                "~/Scripts/angular/testAngularController.js"
            ));

            // When in debug configuration bundling is disabled by default and enabled for release.
            // This can however be forcefully changed by setting the EnableOptimizations.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=301862
            // BundleTable.EnableOptimizations = true; // true: bundling is on, false: bundling is off
        }
    }
}
