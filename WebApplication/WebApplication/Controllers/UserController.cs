using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using WebModels.Domain;
using WebServices;
using WebCommons;

namespace WebApplication.Controllers
{
    public class UserController : Controller
    {
        private UserService UserService;
      
        public JsonResult GetUser(string id)
        {
            return Json(UserService.getUser(id),JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateUser()
        {
            var dic = HttpUtils.RequestDictionary(Request);
            return Json(UserService.updateUser(dic), JsonRequestBehavior.AllowGet);
        }

        public UserController(UserService userService) {
            this.UserService = userService;
        }
    }
}