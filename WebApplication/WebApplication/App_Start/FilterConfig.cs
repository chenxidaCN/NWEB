using NLog;
using System.Web;
using System.Web.Mvc;

namespace WebApplication
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ExceptionHandlerAttribute());
        }
    }
    public class ExceptionHandlerAttribute : HandleErrorAttribute
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            logger.Error(filterContext.Exception.ToString());
        }
    }
}
