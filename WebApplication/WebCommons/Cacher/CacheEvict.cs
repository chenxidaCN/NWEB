using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCommons.Cacher
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CacheEvict : Attribute
    {
        public string CacheName { get; set; }
        /// <summary>
        /// 范例：#p0,#id,#p0.id,#user.id
        /// </summary>
        public string Key { get; set; }
    }
}
