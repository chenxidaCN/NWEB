using Castle.DynamicProxy;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections;
using ServiceStack.Redis;
using WebCommons.Cacher;

namespace WebCommons
{
    public class CacherInterceptor : IInterceptor
    {
        /// <summary>
        /// 日志记录器
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Redis客户端
        /// </summary>
        private RedisClient Redis { get; set; }
        public void Intercept(IInvocation invocation)
        {
            Logger.Debug("{0}.{1}缓存服务", invocation.TargetType.FullName, invocation.Method.Name);
            try
            {
                var method = invocation.MethodInvocationTarget ?? invocation.Method;
                Cacheable[] cacheables = method.GetCustomAttributes(typeof(Cacheable), true) as Cacheable[];
                //取缓存
                var redisKey = "";
                if (cacheables.Count()>0)
                {
                    var cacheName = cacheables[0].CacheName;
                    var key = cacheables[0].Key;
                    var realkey = ParseEL(key, invocation.Arguments);
                    if (!string.IsNullOrEmpty(realkey))
                    {
                        redisKey = cacheName +":"+ realkey;
                        var redisValue = Redis.Get<string>(redisKey);
                        if (!string.IsNullOrEmpty(redisValue))
                        {
                            invocation.ReturnValue = JsonConvert.DeserializeObject(redisValue, invocation.Method.ReturnType);
                            Logger.Debug("{0}.{1}取缓存成功,结果：{2}", invocation.TargetType.FullName, invocation.Method.Name, JsonConvert.SerializeObject(invocation.ReturnValue));
                            return;
                        }
                    }
                }
                //执行目标方法
                invocation.Proceed();
                //增、改缓存
                if (cacheables.Count()>0 && invocation.ReturnValue != null){
                    Redis.Add<string>(redisKey, JsonConvert.SerializeObject(invocation.ReturnValue));
                    Logger.Debug("{0}.{1}增缓存成功,键值：{2}", invocation.TargetType.FullName, invocation.Method.Name, redisKey);
                }
                CachePut[] cachePuts = method.GetCustomAttributes(typeof(CachePut), true) as CachePut[];
                if (cachePuts.Count() > 0 && invocation.ReturnValue != null)
                {
                    var cacheName = cachePuts[0].CacheName;
                    var key = cachePuts[0].Key;
                    var realkey = ParseEL(key, invocation.Arguments);
                    if (!string.IsNullOrEmpty(realkey))
                    {
                        redisKey = cacheName + ":" + realkey;
                        Redis.Add<string>(redisKey, JsonConvert.SerializeObject(invocation.ReturnValue));
                        Logger.Debug("{0}.{1}改缓存成功,键值：{2}", invocation.TargetType.FullName, invocation.Method.Name, redisKey);
                    }
                    var realkey2 = ParseEL(key, invocation.ReturnValue);
                    if (!string.IsNullOrEmpty(realkey2))
                    {
                        redisKey = cacheName + ":" + realkey2;
                        Redis.Add<string>(redisKey, JsonConvert.SerializeObject(invocation.ReturnValue));
                        Logger.Debug("{0}.{1}改缓存成功,键值：{2}", invocation.TargetType.FullName, invocation.Method.Name, redisKey);
                    }
                }
                CacheEvict[] cacheEvicts = method.GetCustomAttributes(typeof(CacheEvict), true) as CacheEvict[];
                //删缓存
                if (cacheEvicts.Count() > 0)
                {
                    var cacheName = cacheEvicts[0].CacheName;
                    var key = cacheEvicts[0].Key;
                    var realkey = ParseEL(key, invocation.Arguments);
                    if (!string.IsNullOrEmpty(realkey))
                    {
                        redisKey = cacheName + ":" + realkey;
                        Redis.Remove(redisKey);
                        Logger.Debug("{0}{1}删缓存成功,键值：{2}", invocation.TargetType.FullName, invocation.Method.Name, redisKey);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public CacherInterceptor(RedisClient redis)
        {
            this.Redis = redis;
        }
        private string ParseEL(string el,object[] ps){
            if (el.Contains("#p"))
            {
                var num = el.Substring(2);
                int n = int.Parse(num);
                return Convert.ToString(ps[n]);
            }

            return "";
        }
        private string ParseEL(string el, object r)
        {
            if (el.Contains("#result"))
            {
                var p = el.Split('.')[1];
                Type t = r.GetType();
                var o = t.GetProperty(p).GetValue(r, null);
                return Convert.ToString(o);
            }
            return "";
        }
    }
}
