using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApplication.Filters;

namespace WebApplication
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            var json = config.Formatters.JsonFormatter;
            // 解决json序列化时的循环引用问题
            json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            // 移除XML序列化器
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Filters.Add(new WebApiExceptionAttribute());
        }
    }
}
