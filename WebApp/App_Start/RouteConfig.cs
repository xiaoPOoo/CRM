using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",//可以先将此controller调试的时候修改为自己的想要调试的controller
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
                //路由规则，controller是默认的控制器，action是默认的执行的方法
            );
        }
    }
}
