using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rody_sys.Areas.adminPanel.Models;
using rody_sys.Areas.adminPanel.Controllers;
using System.Data.Entity;
using System.Threading;
using System.Globalization;
using System.Threading.Tasks;
using System.Web.Security;

namespace rody_sys.Areas.adminPanel.Controllers
{
    public class visitorController : generalController
    {
                
        //
        // GET: /adminPanel/visitor/
        public ActionResult login()
        {
            return View();
        }
//========================================================
        public JsonResult loginDB(string username, string pass)
        {
            /*
             success = 2 => Blocked
             success = 1 => Active
             success = 0 => Not found           
             */

            try
            {
                admins d = DB.admins.Where(x => x.username == username && x.password == pass).SingleOrDefault();
                if (d != null)
                {
                    if (d.isActive == 0)
                    {
                        //blocked
                        return Json(new { success = 2 }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        //tmam
                        Session["adId"] = d.id.ToString();
                        Session["adName"] = d.name.ToString();
                        Session["adType"] = d.adType.ToString();

                        if(d.adType==1)
                        {
                            //superAdmin
                            return Json(new { success = 11 }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            //admin
                            return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                else
                {
                    //not found
                    return Json(new { success = 0 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { success = -1 }, JsonRequestBehavior.AllowGet);
            }


           
        }
//========================================================
        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("login");
        }
//========================================================

        public int getCurrentAdminId()
        {      
            return int.Parse(Session["adId"].ToString());
        }

//========================================================
	}
}