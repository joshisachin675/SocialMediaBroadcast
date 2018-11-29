using smartData.Controllers;
using System.Web.Mvc;
using System.Web.Routing;

namespace smartData
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.IgnoreRoute("elmah.axd");

            routes.MapRoute(
                name: "LOgifsdsdsdsddsfsfsfdssdsfsns",
                url: "Login",
               defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                namespaces: new string[] { "smartdata.Controllers" }
            );


          

            //routes.MapRoute(
            //    name: "Default",
            //    url: "Users/{action}/{id}",
            //    defaults: new { area = "Users", controller = "Account", action = "Login", id = UrlParameter.Optional },
            //    namespaces: new string[] { "smartdata.Areas.Users.Controllers" }
            //);

            //routes.MapRoute(
            //    name: "Default",
            //    // url: "{controller}/{action}/{id}",
            //   url: "users/{id}",
            //    defaults: new { area = "Users", controller = "Account", action = "Login", id = UrlParameter.Optional },
            //    namespaces: new string[] { "smartData.Areas.Users.Controllers" }
            //);

           
        }
    }
}
