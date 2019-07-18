﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRepositories.Impl
{
    public class BaseDaoImpl<T> : BaseDao<T> where T: class,new()
    {
        private SqlSugarClient Db;
        public SimpleClient<T> CurrentDb { get { return new SimpleClient<T>(Db); } }


        public bool Save(T obj)
        {
            return CurrentDb.Insert(obj);
        }

        public bool Delete(T obj)
        {
            return CurrentDb.Delete(obj);
        }

        public bool Update(T obj)
        {
            return CurrentDb.Update(obj);
        }

        public T Get(string id)
        {
            return CurrentDb.GetById(id);
        }

        public List<T> List(object o)
        {
            return CurrentDb.GetList();
        }
        public BaseDaoImpl(SqlSugarClient ssc)
        {
            this.Db = ssc;
        }
    }
}
