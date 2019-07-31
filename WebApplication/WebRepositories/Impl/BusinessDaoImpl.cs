using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCommons;

namespace WebRepositories.Impl
{
    public class BusinessDaoImpl<T> : BaseDao<T> where T: class,new()
    {
        public bool Save(T obj)
        {
            return GetDb().Insert(obj);
        }

        public bool Delete(T obj)
        {
            return GetDb().Delete(obj);
        }

        public bool Update(T obj)
        {
            return GetDb().Update(obj);
        }
        public bool Update(Dictionary<string, object> dic)
        {
            return AutofacUtils.Resolve<BusinessSqlSugarClient>().Updateable<T>(dic).ExecuteCommand() > 0;
        }
        public T Get(string id)
        {
            return GetDb().GetById(id);
        }

        public List<T> List(object o)
        {
            return GetDb().GetList();
        }

        private SimpleClient<T> GetDb()
        {
            SqlSugarClient Db = AutofacUtils.Resolve<BusinessSqlSugarClient>();
            return new SimpleClient<T>(Db);
        }
    }
    public class BusinessSqlSugarClient : SqlSugarClient
    {
        public BusinessSqlSugarClient(ConnectionConfig config): base(config)
        {

        }
    }
}
