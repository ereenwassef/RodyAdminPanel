using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rody_sys.Areas.adminPanel.Models;
using System.Data.Entity;
using System.Threading;
using System.Globalization;

namespace rody_sys.Areas.adminPanel.Controllers
{
    public class generalController : Controller
    {

       public Rody_DBEntities3 DB = new Rody_DBEntities3();

       // ********** not work ********
       //protected override void OnActionExecuting(ActionExecutingContext filterContext)
       //{
       //    try
       //    {
       //        ViewBag.sesId = Session["adId"];

       //        admins d = DB.admins.Find(Session["adId"]);

       //        ViewBag.adName = d.name;

       //        int super = (int)d.adType;

       //        if (super == 1)
       //        {
       //            ViewBag.isSuper = 1;
       //        }
       //        else
       //        {
       //            ViewBag.isSuper = 0;
       //        }

       //        base.OnActionExecuting(filterContext);
       //    }
       //    catch (Exception e)
       //    {
       //    }
       //}


//========================================================================

        public void addAdminOperation(string opType,int opId, string tblName,string shortString)
        {
            visitorController vist = new visitorController();

           // int adminId = vist.getCurrentAdminId();


            int adminId = int.Parse(Session["adId"].ToString());

            adminOperations adOp = new adminOperations();
            adOp.adminId = adminId;
            adOp.opType = opType;
            adOp.opId = opId;
            adOp.tblName = tblName;
            adOp.shortString = shortString;
            adOp.time = DateTime.Now.ToString("dd/MM/yyyy - hh:mm:ss");

            DB.adminOperations.Add(adOp);
            DB.SaveChanges();            
        }

//========================================================================

	}
}