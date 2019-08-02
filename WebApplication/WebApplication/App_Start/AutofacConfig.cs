using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebCommons;

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
            //数据源的注入
            SqlSugarConfig.RegisterDbs(builder);
            RedisConfig.RegisterRedis(builder);
            //Interceptors的注入
            builder.RegisterAssemblyTypes(Assembly.Load("WebCommons"))
                .Where(t => t.Name.EndsWith("Interceptor"))
                .SingleInstance()
                .PropertiesAutowired();
            //Repositories的注入
            builder.RegisterAssemblyTypes(Assembly.Load("WebRepositories"))
                .Where(t => t.Name.EndsWith("DaoImpl"))
                .AsImplementedInterfaces()
                .SingleInstance()
                .PropertiesAutowired();
            //Services的注入
            builder.RegisterAssemblyTypes(Assembly.Load("WebServices"))
                .Where(t => t.Name.EndsWith("ServiceImpl"))
                .AsImplementedInterfaces()
                .SingleInstance()
                .PropertiesAutowired()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(LoggerInterceptor),typeof(CacherInterceptor));

            var container = builder.Build();
            //设置依赖注入解析器
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //容器实例
            AutofacUtils.Container = container;
            AutofacUtils.Types = Assembly.Load("WebServices").GetTypes().ToList();
        }
    }
}
