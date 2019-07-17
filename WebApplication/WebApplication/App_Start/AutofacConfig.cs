using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication
{
    public class AutofacConfig
    {
        public static void RegisterAll()
        {
            var builder = new ContainerBuilder();

            //注册MvcApplication程序集中所有的控制器
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            //注册MvcApplication程序集中所有的过滤器
            builder.RegisterFilterProvider();
            //注册仓储层服务
            /*
            builder.RegisterType<PostRepository>().As<IPostRepository>();
            builder.RegisterType<PostService>().As<IPostService>();
            */

            //Repositories的注入
            builder.RegisterAssemblyTypes(Assembly.Load("WebRepositories"))
                .Where(t => t.Name.EndsWith("DaoImpl"))
                .AsImplementedInterfaces()
                .InstancePerRequest()
                .PropertiesAutowired();
            //Services的注入
            builder.RegisterAssemblyTypes(Assembly.Load("WebServices"))
                .Where(t => t.Name.EndsWith("ServiceImpl"))
                .AsImplementedInterfaces()
                .InstancePerRequest()
                .PropertiesAutowired();

            var container = builder.Build();

            //设置依赖注入解析器
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
