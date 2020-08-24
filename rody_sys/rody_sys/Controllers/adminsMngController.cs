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

namespace rody_sys.Areas.adminPanel.Controllers
{
    public class adminsMngController : generalController
    {
        
        public ActionResult getAllAdmins()
        {
            return View();
        }
//============================================================
        public ActionResult getAdminOps(int adId)
        {
            ViewBag.adId = adId;
            return View();
        }
//=============================================================

        [HttpGet]
        public JsonResult get_all_admins()
        {
            return Json(new
            {
                allAdmins = DB.admins.Select(d => new { d.id, d.name, d.pic, d.isActive }).ToList()
            }, JsonRequestBehavior.AllowGet);
        }
//=============================================================
        [HttpGet]
        public JsonResult get_admin_ops(int adId)
        {
            return Json(new
            {
                allOps = DB.adminOperations.Where(d=>d.adminId==adId).Select(d => new { d.id, d.adminId, d.opId, d.shortString , d.time }).ToList()
               , DB.admins.Where(d=>d.id==adId).Single().name
            }, JsonRequestBehavior.AllowGet);
        }
//=============================================================
        [HttpPost]
        public JsonResult delete_admin(int id)
        {
            try
            {
                admins a = DB.admins.Where(x => x.id == id).Single();
                DB.admins.Remove(a);
                DB.SaveChanges();
                
                return Json(new { msg = "تمت حذف الادمن بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه الحذف .. حاول مره اخري" });
            }
        }
//=============================================================
        [HttpPost]
        public void setActiveState(int id)
        {

            admins ad = DB.admins.Where(a => a.id == id).Single();
            ad.isActive = 1;
            DB.Entry(ad).State = EntityState.Modified;
            DB.SaveChanges();
        }
//==============================================================
         [HttpPost]
        public void setBlockedState(int id)
        {

            admins ad = DB.admins.Where(a => a.id == id).Single();
            ad.isActive = 0;
            DB.Entry(ad).State = EntityState.Modified;
            DB.SaveChanges();
        }
//=============================================================
         [HttpPost]
         public JsonResult add_admin(string name , string usName , string password)
         {
             if (DB.admins.Where(d => d.username == usName).SingleOrDefault() == null)
             {
             try
             {              
                 admins new_ad = new admins();
                 new_ad.name = name;
                 new_ad.username = usName;
                 new_ad.password = password;
                 new_ad.adType = 0;
                 new_ad.isActive = 1;                

                 //if (file != null)
                 //{
                 //    var strFileLocation = "~/assets/admin/img/" + file.FileName;
                 //    file.SaveAs(HttpContext.Server.MapPath(strFileLocation));
                 //    new_ad.pic = file.FileName;
                 //}
                 //else
                 //{
                     new_ad.pic = "adminLogo.png";
                 //}

                 DB.admins.Add(new_ad);
                 DB.SaveChanges();

                 return Json(new { msg = "تمت اضافه الادمن بنجاح" });
             }
             catch
             {
                 return Json(new { msg = "لم تتم عمليه الاضافه .. حاول مره اخري" });
             }
         }else
         {
             return Json(new { msg = "اسم المستخدم موجود بالفعل .. حاول مره اخري" });
         }
      }
//============================================================







	}
}