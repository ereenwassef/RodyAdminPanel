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
    public class servicesController : generalController
    {

        public ActionResult getAllServices()
        {
            return View();
        }
//==========================================================    
        [HttpGet]
        public JsonResult get_all_services()
        {
            return Json(new { allServices = DB.services.Select(s => new { s.id, s.name, s.sharehaId,shName=s.shareha.name , s.sellPrice}).ToList() }, JsonRequestBehavior.AllowGet);
        }
//==========================================================
        [HttpPost]
        public JsonResult add_service(string name,float sellPrice,int sharehaId)
        {
            try
            {
                services ser = new services();
                ser.name = name;
                ser.sellPrice = Math.Round(sellPrice, 5);
                ser.sharehaId = sharehaId;

                DB.services.Add(ser);
                DB.SaveChanges();

                addAdminOperation("w", ser.id, "services", "اضافه خدمه جديده");
                return Json(new {msg="تمت اضافه الخدمه بنجاح" });
            }catch
            {
                return Json(new { msg = "لم تتم اضافه الخدمه .. حاول مره اخري" });
            }

        }
//==========================================================
        [HttpPost]
        public JsonResult update_services(int id,string name, float sellPrice, int sharehaId)
        {
            try
            {
                services ser = DB.services.Where(s => s.id == id).Single();
                ser.name = name;
                ser.sellPrice = Math.Round(sellPrice,5);
                ser.sharehaId = sharehaId;

                DB.Entry(ser).State = EntityState.Modified;
                DB.SaveChanges();

                addAdminOperation("u", ser.id, "services", "تعديل خدمه");
                return Json(new { msg = "تمت تعديل بيانات الخدمه بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم  عمليه الاضافه .. حاول مره اخري" });
            }

        }
//==========================================================
        [HttpPost]
        public JsonResult delete_service(int id)
        {
            try
            {
                services ser = DB.services.Where(s => s.id == id).Single();
                DB.services.Remove(ser);
                DB.SaveChanges();

                addAdminOperation("d", ser.id, "services", "حذف خدمه");
                return Json(new { msg = "تمت حذفه الخدمه بنجاح" });
            }
            catch
            {
                return Json(new { msg = "لم تتم  عمليه الحذف .. حاول مره اخري" });
            }

        }

//===========================================================
        public ActionResult servicesChart()
        {
            return View();
        }
//===========================================================
        [HttpGet]
        public JsonResult servicesChart_get_top_5(string startDate="", string endDate="")
        {

            if ((startDate != "undefined" && startDate != "") && (endDate == "undefined" || endDate == "" || endDate == null))
            {
                DateTime SDate = DateTime.Parse(startDate, new CultureInfo("ar-AE"));

                return Json(new { top5 = DB.operations_client.ToList().Where(s => DateTime.Parse(s.actual_date, new CultureInfo("ar-AE")) >= SDate && s.serviceId != null).GroupBy(op => new { op.serviceId, op.services.name }).Select(o => new { count = o.Count(), o.FirstOrDefault().services.name }).OrderByDescending(x => x.count).Take(5) }, JsonRequestBehavior.AllowGet);

            }
            else if ((startDate == "undefined" || startDate == "") && (endDate != "undefined" && endDate != ""))
            {
                DateTime EDate = DateTime.Parse(endDate, new CultureInfo("ar-AE"));
                return Json(new { top5 = DB.operations_client.ToList().Where(s => DateTime.Parse(s.actual_date, new CultureInfo("ar-AE")) <= EDate && s.serviceId != null).GroupBy(op => new { op.serviceId, op.services.name }).Select(o => new { count = o.Count(), o.FirstOrDefault().services.name }).OrderByDescending(x => x.count).Take(5) }, JsonRequestBehavior.AllowGet);
            }
            else if ((startDate != "undefined" && startDate != "") && (endDate != "undefined" && endDate != ""))
            {
                DateTime SDate = DateTime.Parse(startDate, new CultureInfo("ar-AE"));
                DateTime EDate = DateTime.Parse(endDate, new CultureInfo("ar-AE"));
                return Json(new { top5 = DB.operations_client.ToList().Where(s => (DateTime.Parse(s.actual_date, new CultureInfo("ar-AE")) >= SDate && DateTime.Parse(s.actual_date, new CultureInfo("ar-AE")) <= EDate) && s.serviceId != null).GroupBy(op => new { op.serviceId, op.services.name }).Select(o => new { count = o.Count(), o.FirstOrDefault().services.name }).OrderByDescending(x => x.count).Take(5) }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { top5 = DB.operations_client.Where(s => s.serviceId != null).GroupBy(op => new { op.serviceId, op.services.name }).Select(o => new { count = o.Count(), o.FirstOrDefault().services.name }).OrderByDescending(x => x.count).Take(5) }, JsonRequestBehavior.AllowGet);

            }
        }

//===========================================================

	}
}