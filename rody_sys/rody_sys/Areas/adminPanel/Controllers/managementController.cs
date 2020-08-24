using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rody_sys.Areas.adminPanel.Controllers
{
   // [Authorize]
    public class managementController : Controller
    {
        
        public ActionResult controlPage()
        {
            return View();
        }

        public ActionResult intro()
        {
            return View();
        }

        public ActionResult scriptPartial()
        {
            return View("_scriptPartial");
        }


        [HttpGet]
        public ActionResult page_2()
        {
            return PartialView();
        }


         [HttpGet]
         public ActionResult page_1()
         {
             return PartialView();
         }


	}
}