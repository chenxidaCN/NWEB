﻿using Autofac;
using SqlSugar;
using System.Web;
using System.Web.Mvc;
using WebRepositories.Impl;

namespace WebApplication
{
    public class SqlSugarConfig
    {
        public static void RegisterDbs(ContainerBuilder builder)
        {
            var conStr = "Data Source=服务器地址;port=3306; Initial Catalog=库名;uid=账号; pwd=密码";
            var conStr2 = @"server=DESKTOP-OBVD1G0\MSSQLSERVER2;uid=sa;pwd=123456;database=PLL_ERP_Co_02";
            var conStr2_2 = @"server=DESKTOP-OBVD1G0\MSSQLSERVER2;uid=sa;pwd=123456;database=PLL_ERP_Co_05";
            var conStr3 = @"Data Source=localhost;port=3306; Initial Catalog=base;uid=root; pwd=123456";
            builder.Register(ssc => new BaseSqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = conStr2,
                DbType = DbType.SqlServer,
                InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
                IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了
            })).InstancePerLifetimeScope();
            builder.Register(ssc => new BusinessSqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = conStr2_2,
                DbType = DbType.SqlServer,
                InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
                IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了
            })).InstancePerLifetimeScope();
        }
    }
}
