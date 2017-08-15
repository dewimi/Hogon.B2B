using System.Web;
using System.Web.Optimization;

namespace Hogon.Store.UserInterface.Admin
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                      "~/Scripts/angular.js"));

            bundles.Add(new ScriptBundle("~/bundles/hogon/common").Include(
                      "~/Scripts/hogon/common.js"));

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                      "~/Scripts/toastr.js"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
  
                   bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                     "~/Scripts/fileinput.js"
                     , "~/Content/bootstrap-fileinput/themes/explorer/theme.js"
                     , "~/Scripts/bootstrap.js"
                     ,"~/Scripts/respond.js"
                     , "~/Scripts/moment-with-locales.js"
                     , "~/Scripts/smalot-datetimepicker/bootstrap-datetimepicker.js"
                     , "~/Scripts/bootstrap-paginator.js"
                     , "~/Scripts/bootbox.js"
                     , "~/Scripts/smalot-datetimepicker/locales/bootstrap-datetimepicker.zh-CN.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"
                      , "~/Content/bootstrap-fileinput/css/fileinput.css"
                      , "~/Content/bootstrap-fileinput/themes/explorer/theme.css"
                      , "~/Content/toastr.css"
                      , "~/Content/smalot-datetimepicker/bootstrap-datetimepicker.css"));

             bundles.Add(new ScriptBundle("~/Content/css/form").Include(
                  //"~/Content/css/form.css"
                   "~/Content/css/index.css"));
        }
    }
}
