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

namespace WebCommons
{
    public class CacherInterceptor : IInterceptor
    {
        /// <summary>
        /// Redis客户端
        /// </summary>
        private RedisClient Redis { get; set; }
        public void Intercept(IInvocation invocation)
        {
            try
            {
                
                //取缓存
                var key = "";
                if (invocation.Arguments.Count() > 0 && invocation.Arguments[0].GetType() == typeof(string))
                {
                    key = (string)invocation.Arguments[0];
                }
                else
                {
                    invocation.Proceed();
                    return;
                }
                var x = invocation.Arguments;
                if (!string.IsNullOrEmpty(key))
                {
                    
                    var returnValue = Redis.Get(key);
                    if (returnValue != null)
                    {
                        invocation.ReturnValue = returnValue;
                        return;
                    }
                }
                invocation.Proceed();
                //增、改缓存
                if (invocation.ReturnValue != null){
                    Redis.Add(key, invocation.ReturnValue);
                }
                //删缓存
                Redis.Remove(key);
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
    }
}
