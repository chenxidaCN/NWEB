using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApplication.Filters
{
    public class WebApiAuthAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // 这是一个基本例子，使用的ASP.NET Forms 身份验证
            var context = HttpContext.Current;
            if (context.User.Identity.IsAuthenticated == false)
            {
                PreUnauthorized(actionContext);
                return;
            }
        }

        private void PreUnauthorized(HttpActionContext actionContext)
        {
            // 如果用户没有登录，则返回一个通用的错误Model
            actionContext.Response = actionContext.Request.CreateResponse(
                HttpStatusCode.OK,
                new 
                {
                    StatusCode = 77,
                    Message = "该操作需要用户登录"
                });
        }
    }
}