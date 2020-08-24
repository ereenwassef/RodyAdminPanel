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
using System.Data;

namespace rody_sys.Areas.adminPanel.Controllers
{
    public class storesController : generalController
    {

   
        [HttpGet]
        public JsonResult get_all_total_charge()
        {
            return Json(new { totalCharge = DB.shareha.Select(t => new { t.id,t.name,t.value}).ToList() }, JsonRequestBehavior.AllowGet);
        }
//==========================================================
        public JsonResult get_shareha_charge(int? serviceId)
        {
            if (serviceId == null)
            {
                return Json(new { shareha_charge = "" , price="" , shId=""}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                float shareha_charge = (float)DB.services.Where(s => s.id == serviceId).Single().shareha.value;

                int shId = DB.services.Where(s => s.id == serviceId).Single().shareha.id;

                return Json(new { shareha_charge = shareha_charge, price = DB.services.Where(s => s.id == serviceId).Single().sellPrice, shId = shId }, JsonRequestBehavior.AllowGet);
            }
        }


//==========================================================
        [HttpPost]
        public JsonResult delete_shareeha(int id)
        {
            try
            {

                shareha tc = DB.shareha.Where(t => t.id == id).Single();
                DB.shareha.Remove(tc);
                DB.SaveChanges();

                return Json(new { msg="تم حذف الشريحه بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه الحذف .. حاول مره اخري" });
            }
        }
//================================================================
        [HttpPost]
        public JsonResult add_shareeha(string name, double value)
        {
            try
            {
                shareha ch = new shareha();
                ch.name = name;
                ch.value = value;

                DB.shareha.Add(ch);
                DB.SaveChanges();

                return Json(new { msg = "تم اضافه الشريحه بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه الاضافه .. حاول مره اخري" });
            }
        }
//================================================================
        [HttpPost]
        public JsonResult update_shareeha(int id, string name, double value)
        {
            try
            {
                shareha ch = DB.shareha.Where(s => s.id == id).Single();
                ch.name = name;
                ch.value = value;

                DB.Entry(ch).State = EntityState.Modified;
                DB.SaveChanges();

                return Json(new { msg = "تم تعديل بيانات الشريحه بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم عمليه التعديل .. حاول مره اخري" });
            }
        }
//=============================================================================

        public ActionResult getAllStores()
        {
            return View();
        }      
//============================================================
        [HttpGet]
        public JsonResult get_store_value(int storeId)
        {
            return Json(new { store_value = DB.stores.Where(s => s.id == storeId).Single().value }, JsonRequestBehavior.AllowGet);
        }
//============================================================

        [HttpGet]
        public JsonResult get_all_stores()
        {
            return Json(new { allStores = DB.stores.Select(s => new { s.id, s.name , s.value }).ToList() }, JsonRequestBehavior.AllowGet);
        }
//=========================================================

	}
}