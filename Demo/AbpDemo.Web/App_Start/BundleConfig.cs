using System.Web;
using System.Web.Optimization;

namespace AbpDemo.Web
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备就绪，请使用 https://modernizr.com 上的生成工具仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            //VENDOR RESOURCES

            //~/Bundles/vendor/css
            bundles.Add(
                new StyleBundle("~/Bundles/vendor/css")
                .Include("~/fonts/roboto/roboto.css", new CssRewriteUrlTransform())
                .Include("~/fonts/material-icons/materialicons.css", new CssRewriteUrlTransform())
                .Include("~/lib/bootstrap/dist/css/bootstrap.css", new CssRewriteUrlTransform())
                .Include("~/lib/bootstrap-select/dist/css/bootstrap-select.css", new CssRewriteUrlTransform())
                .Include("~/lib/toastr/toastr.css", new CssRewriteUrlTransform())
                .Include("~/lib/sweetalert/dist/sweetalert.css", new CssRewriteUrlTransform())
                .Include("~/lib/famfamfam-flags/dist/sprite/famfamfam-flags.css", new CssRewriteUrlTransform())
                .Include("~/lib/font-awesome/css/font-awesome.css", new CssRewriteUrlTransform())
                .Include("~/lib/Waves/dist/waves.css", new CssRewriteUrlTransform())
                .Include("~/lib/animate.css/animate.css", new CssRewriteUrlTransform())
                .Include("~/css/materialize.css", new CssRewriteUrlTransform())
                .Include("~/css/style.css", new CssRewriteUrlTransform())
                .Include("~/css/themes/all-themes.css", new CssRewriteUrlTransform())
                .Include("~/Views/Shared/_Layout.css", new CssRewriteUrlTransform())
            );

            //~/Bundles/vendor/bottom (Included in the bottom for fast page load)
            bundles.Add(
                new ScriptBundle("~/Bundles/vendor/js/bottom")
                    .Include(
                        "~/lib/json2/json2.js",
                        "~/lib/jquery/dist/jquery.js",
                        "~/lib/bootstrap/dist/js/bootstrap.js",
                        "~/lib/moment/min/moment-with-locales.js",
                        "~/lib/jquery-validation/dist/jquery.validate.js",
                        "~/lib/blockUI/jquery.blockUI.js",
                        "~/lib/toastr/toastr.js",
                        "~/lib/sweetalert/dist/sweetalert-dev.js",
                        "~/lib/spin.js/spin.js",
                        "~/lib/spin.js/jquery.spin.js",
                        "~/lib/bootstrap-select/dist/js/bootstrap-select.js",
                        "~/lib/jquery-slimscroll/jquery.slimscroll.js",
                        "~/lib/Waves/dist/waves.js",
                        "~/lib/push.js/push.js",
                        "~/lib/abp-web-resources/Abp/Framework/scripts/abp.js",
                        "~/lib/abp-web-resources/Abp/Framework/scripts/libs/abp.jquery.js",
                        "~/lib/abp-web-resources/Abp/Framework/scripts/libs/abp.toastr.js",
                        "~/lib/abp-web-resources/Abp/Framework/scripts/libs/abp.blockUI.js",
                        "~/lib/abp-web-resources/Abp/Framework/scripts/libs/abp.spin.js",
                        "~/lib/abp-web-resources/Abp/Framework/scripts/libs/abp.sweet-alert.js",
                        "~/js/admin.js",
                        "~/js/main.js",
                        "~/Views/Shared/_Layout.js",
                        "~/lib/signalr/jquery.signalR.js"
                    )
                );
            bundles.Add(
           new StyleBundle("~/Bundles/account-vendor/css")
               .Include("~/fonts/roboto/roboto.css", new CssRewriteUrlTransform())
               .Include("~/fonts/material-icons/materialicons.css", new CssRewriteUrlTransform())
               .Include("~/lib/bootstrap/dist/css/bootstrap.css", new CssRewriteUrlTransform())
               .Include("~/lib/toastr/toastr.css", new CssRewriteUrlTransform())
               .Include("~/lib/sweetalert/dist/sweetalert.css", new CssRewriteUrlTransform())
               .Include("~/lib/famfamfam-flags/dist/sprite/famfamfam-flags.css", new CssRewriteUrlTransform())
               .Include("~/lib/font-awesome/css/font-awesome.css", new CssRewriteUrlTransform())
               .Include("~/lib/Waves/dist/waves.css", new CssRewriteUrlTransform())
               .Include("~/lib/animate.css/animate.css", new CssRewriteUrlTransform())
               .Include("~/css/materialize.css", new CssRewriteUrlTransform())
               .Include("~/css/style.css", new CssRewriteUrlTransform())
               .Include("~/Views/Account/_Layout.css", new CssRewriteUrlTransform())
           );
            bundles.Add(
                new ScriptBundle("~/Bundles/account-vendor/js/bottom")
                    .Include(
                        "~/lib/json2/json2.js",
                        "~/lib/jquery/dist/jquery.js",
                        "~/lib/bootstrap/dist/js/bootstrap.js",
                        "~/lib/moment/min/moment-with-locales.js",
                        "~/lib/jquery-validation/dist/jquery.validate.js",
                        "~/lib/blockUI/jquery.blockUI.js",
                        "~/lib/toastr/toastr.js",
                        "~/lib/sweetalert/dist/sweetalert-dev.js",
                        "~/lib/spin.js/spin.js",
                        "~/lib/spin.js/jquery.spin.js",
                        "~/lib/Waves/dist/waves.js",
                        "~/lib/abp-web-resources/Abp/Framework/scripts/abp.js",
                        "~/lib/abp-web-resources/Abp/Framework/scripts/libs/abp.jquery.js",
                        "~/lib/abp-web-resources/Abp/Framework/scripts/libs/abp.toastr.js",
                        "~/lib/abp-web-resources/Abp/Framework/scripts/libs/abp.blockUI.js",
                        "~/lib/abp-web-resources/Abp/Framework/scripts/libs/abp.spin.js",
                        "~/lib/abp-web-resources/Abp/Framework/scripts/libs/abp.sweet-alert.js",
                        "~/js/admin.js",
                        "~/js/main.js"
                    )
            );
        }
    }
}
