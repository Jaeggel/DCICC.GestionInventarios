using System.Web;
using System.Web.Optimization;

namespace DCICC.GestionInventarios
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/Tema/vendors/jquery/dist/jquery.min.js",
                        "~/Content/Tema/vendors/bootstrap/dist/js/bootstrap.min.js",
                        "~/Content/Tema/vendors/fastclick/lib/fastclick.js",
                        "~/Content/Tema/vendors/nprogress/nprogress.js",
                        "~/Content/Tema/vendors/Chart.js/dist/Chart.min.js",
                        "~/Content/Tema/vendors/gauge.js/dist/gauge.min.js",
                        "~/Content/Tema/vendors/bootstrap-progressbar/bootstrap-progressbar.min.js",
                        "~/Content/Tema/vendors/iCheck/icheck.min.js",
                        "~/Content/Tema/vendors/skycons/skycons.js",
                        "~/Content/Tema/vendors/Flot/jquery.flot.js",
                        "~/Content/Tema/vendors/Flot/jquery.flot.pie.js",
                        "~/Content/Tema/vendors/Flot/jquery.flot.time.js",
                        "~/Content/Tema/vendors/Flot/jquery.flot.stack.js",
                        "~/Content/Tema/vendors/Flot/jquery.flot.resize.js",
                        "~/Content/Tema/vendors/flot.orderbars/js/jquery.flot.orderBars.js",
                        "~/Content/Tema/vendors/flot-spline/js/jquery.flot.spline.min.js",
                        "~/Content/Tema/vendors/flot.curvedlines/curvedLines.js",
                        "~/Content/Tema/vendors/DateJS/build/date.js",
                        "~/Content/Tema/vendors/jqvmap/dist/jquery.vmap.js",
                        "~/Content/Tema/vendors/jqvmap/dist/maps/jquery.vmap.world.js",
                        "~/Content/Tema/vendors/jqvmap/examples/js/jquery.vmap.sampledata.js",
                        "~/Content/Tema/vendors/moment/min/moment.min.js",
                        "~/Content/Tema/vendors/bootstrap-daterangepicker/daterangepicker.js",
                        "~/Content/Tema/build/js/custom.js",
                        "~/Scripts/Site.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                      "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Tema/vendors/bootstrap/dist/css/bootstrap.min.css",
                      "~/Content/Tema/vendors/font-awesome/css/font-awesome.min.css",
                      "~/Content/Tema/vendors/nprogress/nprogress.css",
                      "~/Content/Tema/vendors/iCheck/skins/flat/green.css",
                      "~/Content/Tema/vendors/bootstrap-progressbar/css/bootstrap-progressbar-3.3.4.min.css",
                      "~/Content/Tema/vendors/jqvmap/dist/jqvmap.min.css",
                      "~/Content/Tema/vendors/bootstrap-daterangepicker/daterangepicker.css",
                      "~/Content/Tema/build/css/custom.css",
                      "~/Content/Site.css"));
        }
    }
}
