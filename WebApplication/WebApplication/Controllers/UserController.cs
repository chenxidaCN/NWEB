using System.Web.Mvc;
using WebCommons;
using WebServices;

namespace WebApplication.Controllers
{
    public class UserController : Controller
    {
        private UserService UserService;

        public JsonResult GetUser(string id)
        {
            return Json(UserService.getUser(id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateUser()
        {
            var dic = HttpUtils.ReqToDictionary(Request);
            return Json(UserService.updateUser(dic), JsonRequestBehavior.AllowGet);
        }

        public UserController(UserService userService)
        {
            this.UserService = userService;
        }
    }
}