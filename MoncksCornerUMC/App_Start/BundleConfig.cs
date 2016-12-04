//using System.Web;
//using System.Web.Optimization;

//namespace MoncksCornerUMC
//{
//    public class BundleConfig
//    {
//        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
//        public static void RegisterBundles(BundleCollection bundles)
//        {
//            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
//                        "~/Scripts/jquery-{version}.js"));

//            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
//                        "~/Scripts/jquery.validate*"));

//            // Use the development version of Modernizr to develop with and learn from. Then, when you're
//            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
//            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
//                        "~/Scripts/modernizr-*"));

//            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
//                      "~/Scripts/bootstrap.js",
//                      "~/Scripts/respond.js"));

//            bundles.Add(new StyleBundle("~/Content/css").Include(
//                      "~/Content/bootstrap.css",
//                      "~/Content/site.css"));
//        }
//    }
//}

using System.Web;
using System.Web.Optimization;

namespace MoncksCornerUMC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/assets/plugins/jquery-2.2.3.min.js",
                        "~/assets/plugins/jquery.easing.1.3.js",
                        "~/assets/plugins/jquery.cookie.js",
                        "~/assets/plugins/jquery.appear.js",
                        "~/assets/plugins/jquery.isotope.js",
                        "~/assets/plugins/masonry.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/assets/plugins/modernizr.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/assets/plugins/bootstrap/js/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/plugins").Include(
                            "~/assets/plugins/magnific-popup/jquery.magnific-popup.min.js",
                            "~/assets/plugins/owl-carousel/owl.carousel.min.js",
                            "~/assets/plugins/stellar/jquery.stellar.min.js",
                            "~/assets/plugins/knob/js/jquery.knob.js",
                            "~/assets/plugins/jquery.backstretch.min.js",
                            "~/assets/plugins/superslides/dist/jquery.superslides.min.js",
                            "~/assets/plugins/styleswitcher/styleswitcher.js",// -- STYLESWITCHER - REMOVE ON PRODUCTION/DEVELOPMENT -->
                            "~/assets/plugins/mediaelement/build/mediaelement-and-player.min.js",
                            // -- REVOLUTION SLIDER -->
                            "~/assets/plugins/slider.revolution.v4/js/jquery.themepunch.tools.min.js",
                            "~/assets/plugins/slider.revolution.v4/js/jquery.themepunch.revolution.min.js",
                            "~/assets/js/slider_revolution.js",
                            "~/assets/js/scripts.js"));

            bundles.Add(new StyleBundle("~/content/css").Include(
                          "~/assets/plugins/bootstrap/css/bootstrap.min.css",
                            "~/assets/css/font-awesome.css",
                            "~/assets/plugins/owl-carousel/owl.carousel.css",
                            "~/assets/plugins/owl-carousel/owl.theme.css",
                            "~/assets/plugins/owl-carousel/owl.transitions.css",
                            "~/assets/plugins/magnific-popup/magnific-popup.css",
                            "~/assets/css/animate.css",
                            "~/assets/css/superslides.css",
                            // -- REVOLUTION SLIDER -->
                            "~/assets/plugins/slider.revolution.v4/css/settings.css",
                            // -- THEME CSS -->
                            "~/assets/css/essentials.css",
                            "~/assets/css/layout.css",
                            "~/assets/css/layout-responsive.css",
                            "~/assets/css/color_scheme/orange.css"));   // -- orange: default style -->

            bundles.Add(new StyleBundle("~/styleswitcher/css").Include(
                            // -- styleswitcher - demo only -->
                            "~/assets/css/color_scheme/orange.css",
                            "~/assets/css/color_scheme/red.css",
                            "~/assets/css/color_scheme/pink.css",
                            "~/assets/css/color_scheme/yellow.css",
                            "~/assets/css/color_scheme/darkgreen.css",
                            "~/assets/css/color_scheme/green.css",
                            "~/assets/css/color_scheme/darkblue.css",
                            "~/assets/css/color_scheme/blue.css",
                            "~/assets/css/color_scheme/brown.css",
                            "~/assets/css/color_scheme/lightgrey.css",
                            // -- /styleswitcher - demo only -->
                            // -- STYLESWITCHER - REMOVE ON PRODUCTION/DEVELOPMENT -->
                            "~/assets/plugins/styleswitcher/styleswitcher.css"));
        }
    }
}

