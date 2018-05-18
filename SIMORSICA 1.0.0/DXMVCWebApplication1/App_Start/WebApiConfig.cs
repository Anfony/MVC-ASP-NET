using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DXMVCWebApplication1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "ApiPEF",
                routeTemplate: "api/v1/{controller}/{Anio}"
            );

            config.Routes.MapHttpRoute(
                name: "ApiPresComites",
                routeTemplate: "api/v1/{controller}/{action}/{Anio}/{Id}"
)           ;

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ApiByAction",
                routeTemplate: "api/v1/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}