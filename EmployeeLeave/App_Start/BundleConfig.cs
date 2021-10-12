using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace EmployeeLeave
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new Bundle("~/Scripts/bootstrap").Include(
                      "~/Scripts/jquery-3.6.0.js", "~/Scripts/umd/popper.js", "~/Scripts/bootstrap.js"));

            bundles.Add(new Bundle("~/Styles/bootstrap").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Styles.css"));
            BundleTable.EnableOptimizations = true;
        }
    }
}
