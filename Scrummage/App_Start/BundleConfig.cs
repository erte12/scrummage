using System.Web;
using System.Web.Optimization;

namespace Scrummage
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/bootbox.js",
                        "~/Scripts/datatables/jquery.datatables.js",
                        "~/Scripts/datatables/datatables.bootstrap.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/toastr.js",
                        "~/Scripts/moment.js",
                        "~/Scripts/bootstrap-datetimepicker.js",
                        "~/Scripts/typeahead.bundle.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/highcharts").Include(
                "~/Scripts/highcharts/5.0.14/highcharts.js",
                "~/Scripts/highcharts/5.0.14/modules/data.js",
                "~/Scripts/highcharts/5.0.14/modules/series-label.js",
                "~/Scripts/highcharts/5.0.14/modules/exporting.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/handlebars").Include(
                "~/Scripts/handlebars.min.js",
                "~/Scripts/handlebars-selecthelper.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                "~/Scripts/jquery-ui-1.12.1.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.enter-submit.js",
                        "~/Scripts/jquery.validate*"));

            //Views
            bundles.Add(new ScriptBundle("~/bundles/views/teams/index").Include(
                "~/Scripts/Views/Teams/index.js"));

            bundles.Add(new ScriptBundle("~/bundles/views/teams/details").Include(
                "~/Scripts/Views/Teams/details.js"));

            bundles.Add(new ScriptBundle("~/bundles/views/events/index").Include(
                "~/Scripts/Views/Events/index.js"));

            bundles.Add(new ScriptBundle("~/bundles/views/sprints/index").Include(
                "~/Scripts/Views/Sprints/index.js"));

            bundles.Add(new ScriptBundle("~/bundles/views/sprints/manage").Include(
                "~/Scripts/Views/Sprints/manage.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootswatch/cosmo/bootstrap.min.css",
                      "~/Content/toastr.css",
                      "~/Content/datatables/css/datatables.bootstrap.css",
                      "~/Content/typeahead.css",
                      "~/Content/custom.css",
                      "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/site.css"));
        }
    }
}
