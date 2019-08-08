using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace WebApplication.Filters
{
    public class WebApiExceptionAttribute: ExceptionFilterAttribute
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            logger.Error(actionExecutedContext.Exception);
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(
                HttpStatusCode.OK,
                new 
                {
                    StatusCode = 1,
                    Message = actionExecutedContext.Exception.Message
                });
        }
    }
}