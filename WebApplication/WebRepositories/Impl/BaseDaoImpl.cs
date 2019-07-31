using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels.Domain;

namespace WebRepositories.Impl
{
    public class BaseDaoImpl<T> : BaseDao<T> where T: BaseData,new()
    {
        public SqlSugarClient Db {get;set;}
        public SimpleClient<T> CurrentDb { get { return new SimpleClient<T>(Db); } }


        public bool Save(T obj)
        {
            if (string.IsNullOrEmpty(obj.Id))
            {
                return CurrentDb.Insert(obj);
            }
            return CurrentDb.Update(obj);
        }

        public bool Delete(T obj)
        {
            return CurrentDb.Delete(obj);
        }

        public bool Update(T obj)
        {
            return CurrentDb.Update(obj);
        }
        public bool Update(Dictionary<string,object> dic)
        {
            return Db.Updateable<T>(dic).ExecuteCommand()>0;
        }

        public T Get(string id)
        {
            return CurrentDb.GetById(id);
        }

        public List<T> List(object o)
        {
            return CurrentDb.GetList();
        }
        public BaseDaoImpl(BaseSqlSugarClient ssc)
        {
            this.Db = ssc;
        }
    }
    public class BaseSqlSugarClient : SqlSugarClient{
        public BaseSqlSugarClient(ConnectionConfig config): base(config)
        {

        }
    }
}
