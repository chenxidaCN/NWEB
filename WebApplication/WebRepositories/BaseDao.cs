using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRepositories
{
    interface BaseDao<T> where T : class,new()
    {
        //增
        bool Save(T obj);
        //删
        bool Delete(T obj);
        //改
        bool Update(T obj);
        //查
        T Get(string id);
        List<T> List(Object o);
    }
}
