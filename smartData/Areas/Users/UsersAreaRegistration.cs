using System.Web.Mvc;

namespace smartData.Areas.Users
{
    public class UsersAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Users";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
"Post_12",
"a/{postId}/",
new { Area = "Users", controller = "Post", action = "GetResults", postId = UrlParameter.Optional },
namespaces: new string[] { "smartData.Areas.Users.Controllers" }
);


            context.MapRoute(
                "Users_elmah",
                "Users/elmah/{type}",
                new { action = "Index", controller = "Elmah", type = UrlParameter.Optional });

            context.MapRoute(
                "Users_elmahdetail",
                "Users/elmah/detail/{type}",
                new { action = "Index", controller = "Elmah", type = UrlParameter.Optional }
        );
            context.MapRoute(
                            "Users_elmahabout",
                            "Users/elmah/about/{type}",
                            new { action = "Index", controller = "Elmah", type = UrlParameter.Optional }
                    );
            context.MapRoute(
                "Users_elmahdetailabout",
                "Users/elmah/detail/about/{type}",
                new { action = "Index", controller = "Elmah", type = UrlParameter.Optional }
        );


            context.MapRoute(
    "Login",
    "login",
    new { Area = "Users", controller = "Account", action = "Login", id = UrlParameter.Optional },
    namespaces: new string[] { "smartData.Areas.Users.Controllers" }
);



            context.MapRoute(
    "realtor/login",
    "{realtor}/login",
    new { Area = "Users", controller = "Account", action = "Login", id = UrlParameter.Optional },
    namespaces: new string[] { "smartData.Areas.Users.Controllers" }
);

            context.MapRoute(
"realtor/Register",
"{realtor}/Register",
new { Area = "Users", controller = "Account", action = "Register", id = UrlParameter.Optional },
namespaces: new string[] { "smartData.Areas.Users.Controllers" }
);
            //            context.MapRoute(
            //  "Users_default1",
            //  "Users/{controller}/{action}/{id}",
            //  new { Area = "Users", controller = "Account", action = "Login", id = UrlParameter.Optional },
            //  namespaces: new string[] { "smartData.Areas.Users.Controllers" }
            //);

            //            context.MapRoute(
            //"ResetPassword",
            //"Users/{controller}/{action}/{id}",
            //new { Area = "Users", controller = "Account", action = "ResetPassword", id = UrlParameter.Optional },
            //namespaces: new string[] { "smartData.Areas.Users.Controllers" }
            //);

            context.MapRoute(
"User",
"user",
new { area = "Users", controller = "Home", action = "Index", id = UrlParameter.Optional },
namespaces: new string[] { "smartData.Areas.Users.Controllers" }
);

            context.MapRoute(
"UserDashboardRoute",
"user/dashboard/{id}",
new { area = "Users", controller = "Home", action = "Index", id = UrlParameter.Optional },
namespaces: new string[] { "smartData.Areas.Users.Controllers" }
);
            context.MapRoute(
"ManageSocialMedia",
"SocialMedia/{id}",
new { Area = "Users", controller = "Home", action = "ManageSocialMedia", id = UrlParameter.Optional },
namespaces: new string[] { "smartData.Areas.Users.Controllers" }
);

            context.MapRoute(
     "Facebook",
     "{controller}/{action}/{id}",
     new { Area = "Users", controller = "Home", action = "Index", id = UrlParameter.Optional },
     namespaces: new string[] { "smartData.Areas.Users.Controllers" }
   );

            context.MapRoute(
   "LinkedinCallBack",
   "{controller}/{action}/{id}",
   new { Area = "Users", controller = "Home", action = "OAuth2", id = UrlParameter.Optional },
   namespaces: new string[] { "smartData.Areas.Users.Controllers" }
 );

            context.MapRoute(
 "TwitterCallBack",
 "{controller}/{action}/{id}",
 new { Area = "Users", controller = "Home", action = "AuthorizeCallback", id = UrlParameter.Optional },
 namespaces: new string[] { "smartData.Areas.Users.Controllers" }
);

            context.MapRoute(
"GooglePlusCallBack",
"{controller}/{action}/{id}",
new { Area = "Users", controller = "Home", action = "GoogleCallBack", id = UrlParameter.Optional },
namespaces: new string[] { "smartData.Areas.Users.Controllers" }
);

            context.MapRoute(
"Schedule",
"{controller}/schedule/{id}",
new { Area = "Users", controller = "Home", action = "SchedulePost", id = UrlParameter.Optional },
namespaces: new string[] { "smartData.Areas.Users.Controllers" }
);




           
        }
    }
}