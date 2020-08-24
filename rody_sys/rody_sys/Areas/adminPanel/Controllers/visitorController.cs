using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace rody_sys.Areas.adminPanel.Controllers
{
    public class visitorController : Controller
    {
        //
        // GET: /adminPanel/visitor/
        public ActionResult login()
        {
            return View();
        }

        public ActionResult loginDB(string username,string pass)
        {

            if(username.Equals("aya123") && pass.Equals("123456"))
            {
                FormsAuthentication.SetAuthCookie("aya123", false);

                return RedirectToAction("../management/controlPage");
            }else
            {
                return RedirectToAction("login");
            }
           
        }

        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("login");
        }

        




	}
}