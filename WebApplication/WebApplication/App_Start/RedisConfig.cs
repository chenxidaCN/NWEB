using Autofac;
using ServiceStack.Redis;
using System.Web;
using System.Web.Mvc;
using WebRepositories.Impl;

namespace WebApplication
{
    public class RedisConfig
    {
        public static void RegisterRedis(ContainerBuilder builder)
        {
            //RedisClient redisClient = new RedisClient("123.207.96.138", 6379);
            var host = "127.0.0.1";
            var port = 6379;
            string password = null;
            builder.Register(rc => new RedisClient(host, 6379, password)).InstancePerLifetimeScope();
        }
    }
}
