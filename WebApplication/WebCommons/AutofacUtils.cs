using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCommons
{
    public class AutofacUtils
    {
        public Dictionary<String, ILifetimeScope> LifeManager = new Dictionary<String, ILifetimeScope>();
        public static IContainer Container { get; set; }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
        public void BeginLifetimeScope()
        {
            LifeManager.Add("null", Container.BeginLifetimeScope());
        }
        public void BeginLifetimeScope(string tab)
        {
            LifeManager.Add(tab, Container.BeginLifetimeScope(tab));
        }
        public void Dispose()
       {
           LifeManager["null"].Dispose();
           LifeManager.Remove("null");
        }
        public void Dispose(string tab)
        {
            LifeManager[tab].Dispose();
            LifeManager.Remove(tab);
        }

    }
}
