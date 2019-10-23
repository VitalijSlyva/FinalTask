using System.Web;
using System.Web.Optimization;

namespace Rental.WEB
{
    /// <summary>
    /// Bundle configuration
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// Register main bundles
        /// </summary>
        /// <param name="bundles">Bundle collection</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            RegistreRentBundles(bundles);


            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                  "~/Content/themes/base/core.css",
                  "~/Content/themes/base/resizable.css",
                  "~/Content/themes/base/selectable.css",
                  "~/Content/themes/base/accordion.css",
                  "~/Content/themes/base/autocomplete.css",
                  "~/Content/themes/base/button.css",
                  "~/Content/themes/base/dialog.css",
                  "~/Content/themes/base/slider.css",
                  "~/Content/themes/base/tabs.css",
                  "~/Content/themes/base/datepicker.css",
                  "~/Content/themes/base/progressbar.css",
                  "~/Content/themes/base/theme.css"));
        }

        /// <summary>
        /// Registercustom bundles.
        /// </summary>
        /// <param name="bundles">Bundle collection</param>
        private static void RegistreRentBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/styles/shared/layout")
                .Include("~/Content/styles/shared/layout.css"));
            bundles.Add(new StyleBundle("~/Content/styles/shared/filter")
                .Include("~/Content/styles/shared/filter.css"));

            bundles.Add(new StyleBundle("~/Content/styles/rent/index")
                .Include("~/Content/styles/rent/index.css"));
            bundles.Add(new StyleBundle("~/Content/styles/rent/car")
                .Include("~/Content/styles/rent/car.css"));

            bundles.Add(new StyleBundle("~/Content/styles/client/show-orders")
                .Include("~/Content/styles/client/show-orders.css"));
            bundles.Add(new StyleBundle("~/Content/styles/client/show-payments")
                .Include("~/Content/styles/client/show-payments.css"));

            bundles.Add(new StyleBundle("~/Content/styles/admin/get-users")
                .Include("~/Content/styles/admin/get-users.css"));
            bundles.Add(new StyleBundle("~/Content/styles/admin/get-cars")
                .Include("~/Content/styles/admin/get-cars.css"));
            bundles.Add(new StyleBundle("~/Content/styles/admin/create-car")
                .Include("~/Content/styles/admin/create-car.css"));

            bundles.Add(new StyleBundle("~/Content/styles/manager/show-returns")
                .Include("~/Content/styles/manager/show-returns.css"));
            bundles.Add(new StyleBundle("~/Content/styles/manager/show-confirms")
                .Include("~/Content/styles/manager/show-confirms.css"));
        }
    }
}
