using System.Web;
using System.Web.Optimization;

namespace _0110Work
{
    public class BundleConfig
    {
        // 如需統合的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min*"));

            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好可進行生產時，請使用 https://modernizr.com 的建置工具，只挑選您需要的測試。

            bundles.Add(new ScriptBundle("~/bundles/Js").Include(
                      "~/Scripts/jquery-3.4.1.min.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/modernizr-2.8.3.js",
                      "~/Scripts/jquery.validate.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/Sidebar").Include(
                      "~/Scripts/Sidebar.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css",
                      "~/Content/Sidebar.css"));

            bundles.Add(new StyleBundle("~/Content/Sidebar").Include(
                      "~/Content/Sidebar.css"));

            bundles.Add(new StyleBundle("~/Content/MyCss").Include(
                      "~/Content/MyNavbar.css"));

            bundles.Add(new ScriptBundle("~/bundles/MyJs").Include(
                      "~/Scripts/MyNavbar.js"));
        }
    }
}
