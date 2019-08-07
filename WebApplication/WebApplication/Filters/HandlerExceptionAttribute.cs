using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Filters
{
    public class HandlerExceptionAttribute : HandleErrorAttribute
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            logger.Error(filterContext.Exception.ToString());
        }
    }
}