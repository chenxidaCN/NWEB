using Castle.DynamicProxy;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections;

namespace WebCommons
{
    public class LoggerInterceptor : IInterceptor
    {
        /// <summary>
        /// 日志记录器
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public void Intercept(IInvocation invocation)
        {
            try
            {
                if (Logger.IsDebugEnabled)
                {
                    Logger.Debug("{0}{1}参数：{2}", invocation.TargetType.FullName, invocation.Method.Name, JsonConvert.SerializeObject(invocation.Arguments));
                }
                invocation.Proceed();
                if (Logger.IsDebugEnabled)
                {
                    if (invocation.ReturnValue != null && invocation.ReturnValue is IEnumerable)
                    {
                        dynamic collection = invocation.ReturnValue;
                        Logger.Debug("{0}{1}结果-行数：{2},结果-内容：{3}", invocation.TargetType.FullName, invocation.Method.Name, collection.Count, JsonConvert.SerializeObject(invocation.Arguments));
                    }
                    else
                    {
                        Logger.Debug("{0}{1}结果：{2}", invocation.TargetType.FullName, invocation.Method.Name, JsonConvert.SerializeObject(invocation.ReturnValue));
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, invocation.TargetType.FullName+invocation.Method.Name+"执行异常");
                throw e;
            }
        }
    }
}
