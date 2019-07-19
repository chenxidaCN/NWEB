using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels.Domain;

namespace WebRepositories.Impl
{
    public class UserDaoImpl : BaseDaoImpl<User>,UserDao
    {
        public UserDaoImpl(SqlSugarClient ssc):base(ssc)
        {
            
        }
    }
}
