using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using WebModels.Domain;
using WebServices;

namespace WebApplication.Controllers
{
    public class FormController : Controller
    {
        private FormService FormService;
      
        public JsonResult GetForm(string id)
        {
            return Json(FormService.getForm(id),JsonRequestBehavior.AllowGet);
        }
        public JsonResult TestEval()
        {
            return Json(FormService.Eval(), JsonRequestBehavior.AllowGet);
        }
        public FormController(FormService FormService) {
            this.FormService = FormService;
        }
    }
}