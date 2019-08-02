using Autofac;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCommons
{
    public class AutofacUtils
    {
        public static Dictionary<String, ILifetimeScope> LifeManager = new Dictionary<String, ILifetimeScope>();
        public static IContainer Container { get; set; }

        public static List<Type> Types { get; set; }
        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
        public static void BeginLifetimeScope()
        {
            LifeManager.Add("null", Container.BeginLifetimeScope());
        }
        public static void BeginLifetimeScope(string tab)
        {
            LifeManager.Add(tab, Container.BeginLifetimeScope(tab));
        }
        public static void Dispose()
       {
           LifeManager["null"].Dispose();
           LifeManager.Remove("null");
        }
        public static void Dispose(string tab)
        {
            LifeManager[tab].Dispose();
            LifeManager.Remove(tab);
        }
        public static object Eval(string ecaexpression)
        {
            var li = ecaexpression.Split('.');
            var serviceName = li[0];
            var li2 = li[1].Split('(');
            var methodName = li2[0];
            var parameters = li2[1].TrimEnd(')').Split(',');
            return Execute(serviceName, methodName, parameters);
        }
        public static object Eval(string ecaexpression, params string[] parameters)
        {
            var li = ecaexpression.Split('.');
            var serviceName = li[0];
            var li2 = li[1].Split('(');
            var methodName = li2[0];
            var tmpParameters = li2[1].TrimEnd(')').Split(',');
            var realParameters = new List<string>();
            foreach (var tp in tmpParameters)
            {
                if (tp.Contains("#p"))
                {
                    var num = tp.Substring(2);
                    int n = int.Parse(num);
                    realParameters.Add(parameters[n]);
                }
                else
                {
                    realParameters.Add(tp);
                }
            }
            return Execute(serviceName, methodName, realParameters.ToArray());
        }
        public static object Execute(string serviceName, string methodName, string[] parameters)
        {
            var serviceType = Types.Where(t => t.FullName.EndsWith(serviceName.Substring(0, 1).ToUpper() + serviceName.Substring(1))).FirstOrDefault();
            var service = Container.Resolve(serviceType);
            var method = serviceType.GetMethod(methodName);
            var methodParameters = method.GetParameters();
            if(methodParameters.Count()>0){
                var li = new List<object>();
                var i = 0;
                foreach(var methodParameter in methodParameters){
                    var x = JsonConvert.DeserializeObject(parameters[i], methodParameter.ParameterType);
                    li.Add(x);
                    i++;
                }
                return method.Invoke(service, li.ToArray());
            }
            else{
                return method.Invoke(service,null);
            }
        }
    }
}
