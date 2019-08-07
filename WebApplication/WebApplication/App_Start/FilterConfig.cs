﻿using System.Web;
using System.Web.Mvc;
using WebApplication.Filters;

namespace WebApplication
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new HandlerExceptionAttribute());
        }
    }
    
}
