using smartData.Infrastructure;
using System.Linq;
using System.Web.Http;

namespace smartData
{
    public static class WebApiConfig
    {   
        public static void Register(HttpConfiguration config)
        {
           config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        //    config.Routes.MapHttpRoute(
        //    name: "AdminDefault_Api",
        //    routeTemplate: "Admin/api/{controller}/{action}/{id}",
        //    defaults: new { controller = "AdminHomeApi", id = RouteParameter.Optional }
        //);
            config.Routes.MapHttpRoute(
                    name: "UserDefault_Api",
                    routeTemplate: "Users/api/{controller}/{action}/{id}",
                    defaults: new { controller = "HomeApi", id = RouteParameter.Optional }
                );

            //config.Routes.MapHttpRoute(
            //        name: "AdminDefault_Api",
            //        routeTemplate: "Admin/api/{controller}/{action}/{id}",
            //        defaults: new { controller = "ManageAdminApi", id = RouteParameter.Optional }
            //    );



            config.Filters.Add(new HandleAPIExceptionAttribute());

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);


        }
    }
}
