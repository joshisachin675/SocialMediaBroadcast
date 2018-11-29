using smartData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebMatrix.WebData;
using smartData.Areas.Users;
using System.Globalization;
using System.Reflection;

namespace smartData
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(AreaRegistration)))
           .OrderBy(r => r.FullName)
           .ToList()
           .ForEach(r => RegisterArea(r, RouteTable.Routes, null));
          //  AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            WebSecurity.InitializeDatabaseConnection("AzureDBContext", "Users", "UserId", "Email", true);

            //JobScheduler.Start();  //// Remove comment  when upload on live
            //JodForLikes.Start();  //// Remove comment  when upload on live
            //JodAutoPost.Start(); //// Remove comment  when upload on live

           // WebSecurity.InitializeDatabaseConnection("DefaultConnection", "Users", "UserId", "Email", true);
        }
        public static void RegisterArea(Type t, RouteCollection routes, object state)
        {
            AreaRegistration registration = (AreaRegistration)Activator.CreateInstance(t);
            AreaRegistrationContext context = new AreaRegistrationContext(registration.AreaName, routes, state);
            string tNamespace = registration.GetType().Namespace;
            if (tNamespace != null)
                context.Namespaces.Add(tNamespace + ".*");
            registration.RegisterArea(context);
        }
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            //if (HttpContext.Current.User.IsInRole("admin"))
            //{
            //    // return RedirectToRoute("/elmah.axd");
            //}
        }
        protected void Application_BeginRequest()
        {
            CultureInfo info = new CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.ToString());
            info.DateTimeFormat.ShortDatePattern = "MM-dd-yyyy";
            System.Threading.Thread.CurrentThread.CurrentCulture = info;
        }

    }
}
