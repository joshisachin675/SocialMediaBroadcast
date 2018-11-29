using System.Web.Mvc;

namespace smartData.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {


            context.MapRoute(
"Admin",
"admin",
new { area = "Admin", controller = "Account", action = "Login", id = UrlParameter.Optional },
namespaces: new string[] { "smartData.Areas.Admin.Controllers" }
);

            context.MapRoute(
"superAdmin",
"superadmin",
new { area = "Admin", controller = "Account", action = "SuperAdminLogin", id = UrlParameter.Optional },
namespaces: new string[] { "smartData.Areas.Admin.Controllers" }
);

            context.MapRoute(
"AdminDashboardRoute",
"admin/dashboard/{id}",
new { area = "Admin", controller = "ManageContent", action = "Index", id = UrlParameter.Optional },
namespaces: new string[] { "smartData.Areas.Admin.Controllers" }
);

//            context.MapRoute(
//"ManageAdmin",
//"ManageAdmin/{id}",
//new { Area = "Admin", controller = "ManageAdmin", action = "Index", id = UrlParameter.Optional },
//namespaces: new string[] { "smartData.Areas.Admin.Controllers" }
//);

            context.MapRoute(
               "Admin_default",
               "Admin/{controller}/{action}/{id}",
               new { action = "Index", id = UrlParameter.Optional }
           );

        }
    }
}